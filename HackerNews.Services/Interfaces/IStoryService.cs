using HackerNews.Services.Dto;
namespace HackerNews.Services.Interfaces
{
    public interface IStoryService
    {
       Task<List<StoryDto>> GetTopStoriesAsync(int count);
    }
}
