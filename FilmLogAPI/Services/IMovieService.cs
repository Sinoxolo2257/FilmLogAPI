namespace FilmLogAPI.Services
{
    public interface IMovieService
    {
        Task<Object?> SearchMovieByTitleAsync(string title);
        Task<Object?> GetMovieDetailsByTitleAsync(string title);
    }
}
