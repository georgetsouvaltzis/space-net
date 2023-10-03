using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities;
using Movies.Domain.Repositories;
using Movies.Infrastructure.Db;

namespace Movies.Infrastructure.Repositories;

public class MoviesRepository : IMoviesRepository
{
    private readonly MoviesDbContext _db;
    public MoviesRepository(MoviesDbContext moviesDbContext)
    {
        _db = moviesDbContext;
    }


    public async Task AddBulkAsync(List<Movie> movies)
    {
        await _db.Movies.AddRangeAsync(movies);

        await _db.SaveChangesAsync();
    }


    public async Task<Movie> GetAsync(int movieId)
    {
        return await _db.Movies.FirstAsync(x => x.Id == movieId);
    }

    public async Task UpdateAsync(Movie movie)
    {
        _db.Movies.Update(movie);

        await _db.SaveChangesAsync();
    }

}
