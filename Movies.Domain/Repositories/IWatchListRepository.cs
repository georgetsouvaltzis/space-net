using Movies.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Domain.Repositories
{
    public interface IWatchListRepository
    {
        Task AddAsync();

        Task<WatchList> GetAsync(int watchListId);
    }
}
