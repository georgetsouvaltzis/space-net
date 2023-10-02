using MediatR;
using Movies.Application.Queries;
using Movies.Application.Results;
using Movies.Domain.Repositories;

namespace Movies.Application.Handlers
{
    public class GetUserWatchlistMoviesQueryHandler : IRequestHandler<GetUserWatchlistMoviesQuery, IEnumerable<WatchlistMovieResult>>
    {

        private readonly IUserRepository _userRepository;
        public GetUserWatchlistMoviesQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<WatchlistMovieResult>> Handle(GetUserWatchlistMoviesQuery request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetWatchlistMoviesAsync(request.UserId);

            return result.Select(x => new WatchlistMovieResult
            {
                Id = x.Id,
                Overview = x.Overview,
                Title = x.Title,
                IsWatched = x.IsWatched,
            });
        }
    }
}