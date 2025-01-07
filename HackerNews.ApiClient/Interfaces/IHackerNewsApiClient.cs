using HackerNews.ApiClient.Models;

namespace HackerNews.ApiClient.Interfaces
{
    public interface IHackerNewsApiClient
    {
        Task<IEnumerable<int>> GetBestStoryIdsAsync();
        Task<Story> GetStoryByIdAsync(int id);
    }
}
