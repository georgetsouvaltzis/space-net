using MediatR;
using Movies.Application.Queries;
using Movies.Application.Results;
using Movies.Domain.Entities;
using Movies.Domain.Repositories;
using Movies.Infrastructure.ApiClient;

namespace Movies.Infrastructure.Handlers.Queries
{
    public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, IEnumerable<MovieResult>>
    {
        private readonly ITmdbApiClient _tmDbApiClient;
        private readonly IMoviesRepository _moviesRepository;
        public GetMoviesQueryHandler(ITmdbApiClient tmdbApiClient, IMoviesRepository moviesRepository)
        {
            _tmDbApiClient = tmdbApiClient;
            _moviesRepository = moviesRepository;
        }

        async Task<IEnumerable<MovieResult>> IRequestHandler<GetMoviesQuery, IEnumerable<MovieResult>>.Handle(GetMoviesQuery request, CancellationToken cancellationToken)
        {
            var result = await _tmDbApiClient.GetMoviesAsync(request.Expression);
            
            _moviesRepository.AddBulk(result.Select(x => new Movie
            {
                Id = x.Id,
                Overview = x.Overview,
                Title = x.Title,
            }).ToList());

            return result.Select(x => new MovieResult
            {
                Id = x.Id,
                Overview = x.Overview,
                Title = x.Title,
            });
        }
    }
}
