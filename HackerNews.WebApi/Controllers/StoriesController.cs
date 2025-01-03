using HackerNews.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HackerNews.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoriesController : ControllerBase
    {
        private readonly IStoryService _storyService;

        public StoriesController(IStoryService storyService)
        {
            _storyService = storyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTopStories([FromQuery][Range(1, int.MaxValue, ErrorMessage = "Count must be a positive number.")] int count = 10)
        {
            var stories = await _storyService.GetTopStoriesAsync(count);
            return Ok(stories);
        }
    }
}
