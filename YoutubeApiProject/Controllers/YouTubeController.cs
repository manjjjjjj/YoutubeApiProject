using Microsoft.AspNetCore.Mvc;
using YouTubeApiProject.Services;
using YouTubeApiProject.Models;
namespace YouTubeApiProject.Controllers
{
    public class YouTubeController : Controller
    {
        private readonly YouTubeApiService _youtubeService;
        public YouTubeController(YouTubeApiService youtubeService)
        {
            _youtubeService = youtubeService;
        }
        // Display Search Page
        public IActionResult Index()
        {
            return View(new List<YouTubeVideoModel>());
        }
        // Handle search query
        [HttpPost]
        public async Task<IActionResult> Search(string query, string pageToken = null)
        {
            var (videos, nextPageToken) = await _youtubeService.SearchVideosAsync(query, pageToken);

            // Store query and next page token for pagination
            ViewBag.Query = query;
            ViewBag.NextPageToken = nextPageToken;

            return View("Index", videos);
        }

    }
}