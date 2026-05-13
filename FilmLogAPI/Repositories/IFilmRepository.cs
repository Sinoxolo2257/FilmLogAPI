using FilmLogAPI.Models;

namespace FilmLogAPI.Repositories
{
    public interface IFilmRepository
    {
        Task<IEnumerable<WatchList>> GetAllWatchListsAsync();
        Task<IEnumerable<WatchedList>> GetAllWatchedListsAsync();
        Task<WatchList> AddToWatchListAsync(WatchList watchList);
        Task<WatchedList> AddToWatchedListAsync(WatchedList watchedList);
        Task<WatchList> UpdateWatchListAsync(WatchList watchList);
        Task<WatchedList> UpdateWatchedListAsync(WatchedList watchedList);
        Task<bool> DeleteFromWatchListAsync(int id);
        Task<bool> DeleteFromWatchedListAsync(int id);


    }
}
