using Movies.Domain.Entities;

namespace Movies.Domain.Repositories;

public interface IMoviesRepository
{
    Task AddBulkAsync(List<Movie> movies);
}
