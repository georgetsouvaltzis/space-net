using MediatR;
using Movies.Application.Results;

namespace Movies.Application.Queries
{
    public class GetUserWatchlistMoviesQuery : IRequest<IEnumerable<WatchlistMovieResult>>
    {
        public int UserId { get; set; }
    }
}
