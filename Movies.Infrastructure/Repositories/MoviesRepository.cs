using Movies.Domain.Entities;
using Movies.Domain.Repositories;
using Movies.Infrastructure.Db;

namespace Movies.Infrastructure.Repositories
{
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
    }
}
