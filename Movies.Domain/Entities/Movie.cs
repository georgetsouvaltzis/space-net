namespace Movies.Domain.Entities
{
    public class Movie : BaseEntity
    {
        public string? Title { get; set; }

        public string Overview { get; set; }

        public bool IsWatched { get; set; }

        public WatchList WatchList { get; set; }

        public int WatchListId { get; set; }
    }
}
