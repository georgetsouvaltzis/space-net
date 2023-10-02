namespace Movies.API.Responses;

public class UserWatchlistMoviesResponse
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string Overview { get; set; }
    public bool IsWatched { get; set; }
}
