namespace Movies.Application.Results;

public record class WatchlistMovieResult(int Id, string Overview, string Title, bool IsWatched);
