using Movies.Domain.Entities;

namespace Movies.Domain.Repositories;

public interface IUserRepository
{
    Task AddToWatchlistAsync(int userId, int movieId);

    Task<IEnumerable<Movie>> GetWatchlistMoviesAsync(int userId);

    Task UpdateWatchlistMovieToWatched(int userid, int movieId);
}
