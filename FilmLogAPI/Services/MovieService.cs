using System.Text.Json;

namespace FilmLogAPI.Services
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;

        public MovieService(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClient = httpClientFactory.CreateClient();
            _apiKey = config["OMDb:ApiKey"]!;
            _baseUrl = config["OMDb:BaseUrl"]!;
        }

        public async Task<object?> GetMovieDetailsByTitleAsync(string title)
        {
            var url = $"{_baseUrl}?t={Uri.EscapeDataString(title)}&apikey={_apiKey}";
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            var movieDetails = JsonSerializer.Deserialize<object>(content, new JsonSerializerOptions());
            return movieDetails;
        }

        public async Task<object?> SearchMovieByTitleAsync(string query)
        {
            var url = $"{_baseUrl}?s={Uri.EscapeDataString(query)}&apikey={_apiKey}";
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var content = await response.Content.ReadAsStringAsync();
            var searchResults = JsonSerializer.Deserialize<object>(content, new JsonSerializerOptions());
            return searchResults;

        }
    }
}
