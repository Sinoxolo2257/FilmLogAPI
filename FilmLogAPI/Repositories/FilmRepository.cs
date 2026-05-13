using FilmLogAPI.Models;
using FilmLogAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace FilmLogAPI.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private readonly AppDbContext _context;

        public FilmRepository(AppDbContext context)
        {
            _context = context;
        }

        //Watchlist methods implementation

        public async Task<IEnumerable<WatchList>> GetWatchListsByUserAsync(int userId)
        {
            return await _context.WatchLists.Where(w => w.UserId == userId).ToListAsync();
        }

        public async Task<WatchList> AddToWatchListAsync(WatchList watchList)
        {
            _context.WatchLists.Add(watchList);
            await _context.SaveChangesAsync();
            return watchList;
        }

        public async Task<WatchList> UpdateWatchListAsync(WatchList watchList)
        {
            _context.WatchLists.Update(watchList);
            await _context.SaveChangesAsync();
            return watchList;
        }

        public async Task<WatchList?> GetWatchListItemByTitleAsync(int userId, string title)
        {
            return await _context.WatchLists.FirstOrDefaultAsync(w => w.UserId == userId && w.Title == title);
        }

        public async Task<bool> DeleteFromWatchListAsync(int id, string title)
        {
            var watchlistItem = await GetWatchListItemByTitleAsync(id, title);
            if (watchlistItem == null)
            {
                return false;
            }

            _context.WatchLists.Remove(watchlistItem);
            await _context.SaveChangesAsync();
            return true;
        }

        //WatchedList methods implementation

        public async Task<IEnumerable<WatchedList>> GetWatchedListsByUserAsync(int userId)
        {
            return await _context.WatchedLists.Where(w => w.UserId == userId).ToListAsync();
        }

        public async Task<WatchedList> AddToWatchedListAsync(WatchedList watchedList)
        {
            _context.WatchedLists.Add(watchedList);
            await _context.SaveChangesAsync();
            return watchedList;
        }

        public async Task<WatchedList> UpdateWatchedListAsync(WatchedList watchedList)
        {
            _context.WatchedLists.Update(watchedList);
            await _context.SaveChangesAsync();
            return watchedList;
        }

        public async Task<WatchedList?> GetWatchedItemByIdAsync(int id)
        {
            return await _context.WatchedLists.FirstOrDefaultAsync(w => w.WatchedListId == id);
        }

        public async Task<bool> DeleteFromWatchedListAsync(int id)
        {
            var watchedItem = await GetWatchedItemByIdAsync(id);
            if (watchedItem == null)
            {
                return false;
            }
            _context.WatchedLists.Remove(watchedItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<WatchedList> UpdateMovieCountAsync(int watchedListId, int timesWatched)
        {
            var watchedItem = await GetWatchedItemByIdAsync(watchedListId);
            if (watchedItem == null)
            {
                throw new Exception("Watched item not found");
            }
            watchedItem.TimesWatched = timesWatched;
            _context.WatchedLists.Update(watchedItem);
            await _context.SaveChangesAsync();
            return watchedItem;
        }

        public async Task<WatchedList> ResetMovieCountAsync(int watchedListId)
        {
            var watchedItem = await GetWatchedItemByIdAsync(watchedListId);
            if (watchedItem == null)
            {
                throw new Exception("Watched item not found");
            }
            watchedItem.TimesWatched = 0;
            _context.WatchedLists.Update(watchedItem);
            await _context.SaveChangesAsync();
            return watchedItem;
        }

    }
}
