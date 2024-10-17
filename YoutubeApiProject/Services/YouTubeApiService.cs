using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using YouTubeApiProject.Models;
namespace YouTubeApiProject.Services
{
    public class YouTubeApiService
    {
        private readonly string _apiKey;
        public YouTubeApiService(IConfiguration configuration)
        {
            _apiKey = configuration["YouTubeApiKey"];
        }
        public async Task<(List<YouTubeVideoModel> Videos, string NextPageToken)> SearchVideosAsync(string query, string pageToken = null)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = _apiKey,
                ApplicationName = "YoutubeProject"
            });

            var searchRequest = youtubeService.Search.List("snippet");
            searchRequest.Q = query; // User's query from form input
            searchRequest.MaxResults = 10;
            searchRequest.PageToken = pageToken; // Add page token for pagination

            var searchResponse = await searchRequest.ExecuteAsync();

            var videos = searchResponse.Items.Select(item => new YouTubeVideoModel
            {
                Title = item.Snippet.Title,
                Description = item.Snippet.Description,
                ThumbnailUrl = item.Snippet.Thumbnails.Medium.Url,
                VideoUrl = "https://www.youtube.com/watch?v=" + item.Id.VideoId // Add this line
            }).ToList();

            return (videos, searchResponse.NextPageToken); // Return next page token
        }

    }
}