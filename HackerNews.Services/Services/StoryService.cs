using AutoMapper;
using HackerNews.ApiClient.Interfaces;
using HackerNews.Services.Dto;
using HackerNews.Services.Interfaces;
using HackerNews.Services.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace HackerNews.Services.Services
{
    public class StoryService : IStoryService
    {
        private readonly IHackerNewsApiClient _apiClient;
        private readonly IMapper _mapper;

        public StoryService(IHackerNewsApiClient apiClient, IMapper mapper)
        {
            _apiClient = apiClient;
            _mapper = mapper;
        }
        public async Task<List<StoryDto>> GetTopStoriesAsync(int count)
        {
            var allStoryIds = await _apiClient.GetBestStoryIdsAsync();

            var tasks = allStoryIds.Select(async id =>
            {
                return await _apiClient.GetStoryByIdAsync(id);
            });

            var allStories = await Task.WhenAll(tasks);

            var sortedStories = allStories
                .Where(story => story != null)
                .OrderByDescending(story => story.Score)
                .Take(count)
                .ToList();

            return _mapper.Map<List<StoryDto>>(sortedStories);
        }
    }
}
