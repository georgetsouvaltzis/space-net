using Movies.Domain.Entities;

namespace Movies.Domain.Repositories;

/// <summary>
/// Repository for handling Movie entities.
/// </summary>
public interface IMoviesRepository
{
    /// <summary>
    /// Adds a list of movies into the Database.
    /// </summary>
    /// <param name="movies">List of movies to add.</param>
    Task AddBulkAsync(List<Movie> movies);
    
    /// <summary>
    /// Retrieves movie from the database.
    /// </summary>
    /// <param name="movieId">Movie ID to search for.</param>
    /// <returns></returns>
    Task<Movie> GetAsync(int movieId);

    /// <summary>
    /// Updates existing movie in the database.
    /// </summary>
    /// <param name="movie">Movie to update.</param>
    /// <returns></returns>
    Task UpdateAsync(Movie movie);
}
