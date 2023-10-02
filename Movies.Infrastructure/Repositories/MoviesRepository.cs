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


        public void AddBulk(List<Movie> movies)
        {
            _db.Movies.AddRange(movies);

            _db.SaveChanges(
        }
    }
}
