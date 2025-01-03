using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.ApiClient.Interfaces
{
    public interface IHackerNewsApiClient
    {
        Task<IEnumerable<int>> GetBestStoryIdsAsync();
        //Task<Story> GetStoryByIdAsync(int id);
    }
}
