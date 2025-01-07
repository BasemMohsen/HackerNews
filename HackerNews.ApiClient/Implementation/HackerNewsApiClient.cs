using HackerNews.ApiClient.Configuration;
using HackerNews.ApiClient.Interfaces;
using HackerNews.ApiClient.Models;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace HackerNews.ApiClient.Implementation
{
    public class HackerNewsApiClient : IHackerNewsApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly HackerNewsApiConfig _config;

        public HackerNewsApiClient(HttpClient httpClient, IOptions<HackerNewsApiConfig> config)
        {
            _httpClient = httpClient;
            _config = config.Value;
        }
        public async Task<IEnumerable<int>> GetBestStoryIdsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<int>>(_config.BestStoriesUrl);
        }

        public async Task<Story> GetStoryByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Story>(string.Format(_config.StoryDetailsUrl, id));
        }
    }
}
