using Microsoft.EntityFrameworkCore;
using Movies.Domain.Entities;
using Movies.Domain.Repositories;
using Movies.Infrastructure.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Repositories
{
    public class WatchListRepository : IWatchListRepository
    {
        private readonly MoviesDbContext _dbContext;
        public WatchListRepository(MoviesDbContext moviesDbContext)
        {
            _dbContext = moviesDbContext;
        }

        public Task AddAsync()
        {
            //var user = await _dbContext
            //  .Users
            //  .Include(x => x.WatchList)
            //  .ThenInclude(x => x.Movies)
            //.FirstAsync(x => x.Id == userId);

            //user.WatchList.Movies.Add(new Movie { Id = movieId });

            //await _dbContext.SaveChangesAsync();
            return Task.CompletedTask;
        }

        public async Task<WatchList> GetAsync(int watchListId)
        {
            return await _dbContext.WatchLists.FirstOrDefaultAsync(x => x.Id == watchListId);
        }
    }
}
