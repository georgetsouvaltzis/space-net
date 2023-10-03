using Movies.Domain.Entities;

namespace Movies.Domain.Repositories;

/// <summary>
/// Repository responsible for handling User entities.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Retrieves User watchlist movies.
    /// </summary>
    /// <param name="userId">User ID to retrieve watchlist movies for.</param>
    /// <returns>Collection of found movies.</returns>
    Task<IEnumerable<Movie>> GetWatchlistMoviesAsync(int userId);

    /// <summary>
    /// Retrieves user from the database.
    /// </summary>
    /// <param name="userId">User ID to search for.</param>
    /// <returns>Found user.</returns>
    Task<User> GetUserAsync(int userId);
}
