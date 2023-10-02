using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities;
using Movies.Domain.Repositories;
using Movies.Infrastructure.Db;

namespace Movies.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MoviesDbContext _dbContext;
        public UserRepository(MoviesDbContext moviesDbContext)
        {
            _dbContext = moviesDbContext;
        }

        public async Task AddToWatchlistAsync(int userId, int movieId)
        {
            
            var user = await _dbContext
                .Users
                .Include(x => x.WatchList)
                .ThenInclude(x => x.Movies)
                .FirstAsync(x => x.Id == userId);

            user.WatchList.Movies.Add(new Movie { Id = movieId });

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Movie>> GetWatchlistMoviesAsync(int userId)
        {
            var user = await _dbContext.Users
                .Include(x => x.WatchList)
                .ThenInclude(x => x.Movies)
                .FirstAsync(x => x.Id == userId);

            return user.WatchList.Movies;
        }

        public async Task UpdateWatchlistMovieToWatched(int userid, int movieId)
        {
            var user = await _dbContext
                .Users
                .Include(x => x.WatchList)
                .ThenInclude(x => x.Movies)
                .FirstAsync(x => x.Id == userid);
            var movie = user.WatchList.Movies.First(x => x.Id == movieId);
            movie.IsWatched = true;

            await _dbContext.SaveChangesAsync();

        }
    }
}
