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

        public async Task<User> GetUserAsync(int userId)
        {
            return await _dbContext.Users.Include(x => x.WatchList).ThenInclude(x => x.Movies).FirstAsync(x => x.Id == userId);
        }

        public async Task<IEnumerable<Movie>> GetWatchlistMoviesAsync(int userId)
        {
            var user = await _dbContext.Users
                .Include(x => x.WatchList)
                .ThenInclude(x => x.Movies)
                .FirstAsync(x => x.Id == userId);

            return user.WatchList.Movies;
        }
    }
}
