using Movies.Domain.Entities;

namespace Movies.Domain.Repositories;

public interface IMoviesRepository
{
    Task AddBulkAsync(List<Movie> movies);
    Task<Movie> GetAsync(int movieId);

    Task UpdateAsync(Movie movie);
}
