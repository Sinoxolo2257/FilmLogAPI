using FilmLogAPI.Models;

namespace FilmLogAPI.Repositories
{
    public interface IFilmRepository
    {
        // Watchlist methods
        Task<IEnumerable<WatchList>> GetWatchListsByUserAsync(int userId);
        Task<WatchList> AddToWatchListAsync(WatchList watchList);
        Task<WatchList> UpdateWatchListAsync(WatchList watchList);
        Task<WatchList?> GetWatchListItemByTitleAsync(int userId, string title);
        Task<bool> DeleteFromWatchListAsync(int userId, string title);

        // Watched List methods
        Task<IEnumerable<WatchedList>> GetWatchedListsByUserAsync(int userId);
        Task<WatchedList> AddToWatchedListAsync(WatchedList watchedList);
        Task<WatchedList> UpdateWatchedListAsync(WatchedList watchedList);
        Task<WatchedList?> GetWatchedItemByIdAsync(int id);
        Task<WatchedList> UpdateMovieCountAsync(int watchedListId, int timesWatched);
        Task<bool> DeleteFromWatchedListAsync(int id);
        Task<WatchedList> ResetMovieCountAsync(int watchedListId);
    }
}
