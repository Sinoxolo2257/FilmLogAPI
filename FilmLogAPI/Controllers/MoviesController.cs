using Microsoft.AspNetCore.Mvc;
using FilmLogAPI.Services;

namespace FilmLogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchMovies([FromQuery] string query)
        {
            var results = await _movieService.SearchMovieByTitleAsync(query);
            if (results == null)
            {
                return NotFound("No movies found.");
            }
            return Ok(results);
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetMovieDetails([FromQuery] string title)
        {
            var movieDetails = await _movieService.GetMovieDetailsByTitleAsync(title);
            if (movieDetails == null)
            {
                return NotFound("Movie not found.");
            }
            return Ok(movieDetails);
        }
    }
}
