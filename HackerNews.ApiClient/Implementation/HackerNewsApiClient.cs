using HackerNews.ApiClient.Interfaces;
using HackerNews.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.ApiClient.Implementation
{
    public class HackerNewsApiClient : IHackerNewsApiClient
    {
        private readonly HttpClient _httpClient;

        public HackerNewsApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<int>> GetBestStoryIdsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<int>>("https://hacker-news.firebaseio.com/v0/beststories.json");
        }

        public async Task<Story> GetStoryByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Story>($"https://hacker-news.firebaseio.com/v0/item/{id}.json");
        }
    }
}
