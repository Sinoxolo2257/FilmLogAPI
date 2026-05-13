using Microsoft.AspNetCore.Mvc;
using FilmLogAPI.Repositories;
using FilmLogAPI.Models;
using Microsoft.AspNetCore.Authorization;


namespace FilmLogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WatchlistController : ControllerBase
    {
        private readonly IFilmRepository _filmRepository;

        public WatchlistController(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WatchList>>> GetWatchList()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value ?? "0");
            var watchList = await _filmRepository.GetWatchListsByUserAsync(userId);
            return Ok(watchList);
        }

        [HttpPost]
        public async Task<ActionResult<WatchList>> AddToWatchList(WatchList watchList)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value ?? "0");
            watchList.UserId = userId;
            var addedWatchList = await _filmRepository.AddToWatchListAsync(watchList);
            return CreatedAtAction(nameof(GetWatchList), new { id = addedWatchList.WatchListId }, addedWatchList);
        }

        [HttpDelete("{title}")]
        public async Task<IActionResult> DeleteFromWatchList(string title)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value ?? "0");
            var result = await _filmRepository.DeleteFromWatchListAsync(userId, title);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

       
    }
}