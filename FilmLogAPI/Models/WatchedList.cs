namespace FilmLogAPI.Models
{
    public class WatchedList
    {
        public int UserId { get; set; }
        public int WatchedListId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Genre { get; set; } = string.Empty;
        public string Actor { get; set; } = string.Empty;
        public string Poster { get; set; } = string.Empty;
        public int TimesWatched { get; set; } = 0;
    }
}
