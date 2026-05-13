using Microsoft.AspNetCore.Mvc;
using FilmLogAPI.Repositories;
using FilmLogAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace FilmLogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WatchedListController : ControllerBase
    {
        private readonly IFilmRepository _filmRepository;

        public WatchedListController(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WatchedList>>> GetWatchedList()
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value ?? "0");
            var watchedList = await _filmRepository.GetWatchedListsByUserAsync(userId);
            return Ok(watchedList);
        }

        [HttpPost]
        public async Task<ActionResult<WatchedList>> AddToWatchedList(WatchedList watchedList)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value ?? "0");
            watchedList.UserId = userId;
            var added = await _filmRepository.AddToWatchedListAsync(watchedList);
            return CreatedAtAction(nameof(GetWatchedList), new { id = added.WatchedListId }, added);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WatchedList>> UpdateWatchedList(int id, WatchedList watchedList)
        {
            watchedList.WatchedListId = id;
            var updated = await _filmRepository.UpdateWatchedListAsync(watchedList);
            return Ok(updated);
        }

        [HttpPut("{id}/count")]
        public async Task<ActionResult<WatchedList>> UpdateMovieCount(int id, [FromBody] int timesWatched)
        {
            try
            {
                var updated = await _filmRepository.UpdateMovieCountAsync(id, timesWatched);
                return Ok(updated);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpPost("reset/{id}")]
        public async Task<ActionResult<WatchedList>> ResetMovieCount(int id)
        {
            try
            {
                var reset = await _filmRepository.ResetMovieCountAsync(id);
                return Ok(reset);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFromWatchedList(int id)
        {
            var result = await _filmRepository.DeleteFromWatchedListAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}