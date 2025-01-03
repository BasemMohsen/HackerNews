using HackerNews.Services.Interfaces;
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

        /// <summary>
        /// Retrieves a specified number of top stories.
        /// </summary>
        /// <param name="count">The number of stories to retrieve. Must be greater than 0. Default value is 10.</param>
        /// <response code="200">Returns the requested number of top stories</response>
        /// <response code="400">If count is less than or equal to 0</response>
        [HttpGet]
        public async Task<IActionResult> GetTopStories([FromQuery][Range(1, int.MaxValue, ErrorMessage = "Count must be a positive number.")] int count = 10)
        {
            var stories = await _storyService.GetTopStoriesAsync(count);
            return Ok(stories);
        }
    }
}
