namespace Movies.Application.Results;

public class WatchlistMovieResult
{
    public int Id { get; set; }
    public string Overview { get; set; }
    public string Title { get; set; }
    public bool IsWatched { get; set; }
}