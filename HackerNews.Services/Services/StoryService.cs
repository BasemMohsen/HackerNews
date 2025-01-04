using AutoMapper;
using HackerNews.ApiClient.Interfaces;
using HackerNews.Services.Dto;
using HackerNews.Services.Interfaces;
using HackerNews.Services.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;

namespace HackerNews.Services.Services
{
    public class StoryService : IStoryService
    {
        private readonly IHackerNewsApiClient _apiClient;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;
        private readonly ILogger<StoryService> _logger;

        private const string STORY_IDS_CACHE_KEY = "StoryIds";
        private const string STORY_CACHE_KEY_PREFIX = "Story_";
        private static readonly TimeSpan STORY_IDS_CACHE_DURATION = TimeSpan.FromMinutes(5);
        private static readonly TimeSpan STORY_CACHE_DURATION = TimeSpan.FromMinutes(15);

        public StoryService(IHackerNewsApiClient apiClient, IMapper mapper, IMemoryCache cache, ILogger<StoryService> logger)
        {
            _apiClient = apiClient;
            _mapper = mapper;
            _cache = cache;
            _logger = logger;
        }

        public async Task<List<StoryDto>> GetTopStoriesAsync(int count)
        {
            try
            {
                var allStoryIds = await GetStoryIdsFromCacheOrApiAsync();

                if (!allStoryIds.Any())
                {
                    throw new InvalidOperationException("No story IDs were retrieved.");
                }

                var storiesRetrievalTasks = allStoryIds.Select(GetStoryFromCacheOrApiAsync);
                var allStories = await Task.WhenAll(storiesRetrievalTasks);

                var sortedStories = allStories
                    .Where(story => story != null)
                    .OrderByDescending(story => story.Score)
                    .Take(count)
                    .ToList();

                if (!sortedStories.Any())
                {
                    throw new InvalidOperationException("No valid stories were retrieved after filtering and sorting.");
                }

                return _mapper.Map<List<StoryDto>>(sortedStories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving top stories");
                throw;
            }
        }

        private async Task<IEnumerable<int>> GetStoryIdsFromCacheOrApiAsync()
        {
            if (_cache.TryGetValue(STORY_IDS_CACHE_KEY, out IEnumerable<int> cachedIds))
            {
                _logger.LogInformation("Retrieved story IDs from cache");
                return cachedIds;
            }

            var storyIds = await _apiClient.GetBestStoryIdsAsync();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(STORY_IDS_CACHE_DURATION)
                .SetPriority(CacheItemPriority.High);

            _cache.Set(STORY_IDS_CACHE_KEY, storyIds, cacheEntryOptions);
            _logger.LogInformation("Stored story IDs in cache");

            return storyIds;
        }

        private async Task<Story> GetStoryFromCacheOrApiAsync(int storyId)
        {
            var cacheKey = $"{STORY_CACHE_KEY_PREFIX}{storyId}";

            if (_cache.TryGetValue(cacheKey, out Story cachedStory))
            {
                _logger.LogInformation("Retrieved story {StoryId} from cache", storyId);
                return cachedStory;
            }

            try
            {
                var story = await _apiClient.GetStoryByIdAsync(storyId);

                if (story != null)
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetAbsoluteExpiration(STORY_CACHE_DURATION)
                        .SetPriority(CacheItemPriority.Normal);

                    _cache.Set(cacheKey, story, cacheEntryOptions);
                    _logger.LogInformation("Stored story {StoryId} in cache", storyId);
                }

                return story;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving story {StoryId}", storyId);
                return null;
            }
        }
    }
}
