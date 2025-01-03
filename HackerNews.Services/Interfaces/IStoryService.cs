using HackerNews.Services.Dto;
using HackerNews.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Services.Interfaces
{
    public interface IStoryService
    {
       Task<List<StoryDto>> GetTopStoriesAsync(int count);
    }
}
