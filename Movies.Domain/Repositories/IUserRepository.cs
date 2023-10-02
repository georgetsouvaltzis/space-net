using Movies.Domain.Entities;

namespace Movies.Domain.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<Movie>> GetWatchlistMoviesAsync(int userId);

    Task<User> GetUserAsync(int userId);
}
