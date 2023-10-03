using MediatR;
using Movies.Application.Queries;
using Movies.Application.Results;
using Movies.Infrastructure.ApiClient;

namespace Movies.Infrastructure.Handlers.Queries;

public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, IEnumerable<MovieResult>>
{
    private readonly ITmdbApiClient _tmDbApiClient;
    public GetMoviesQueryHandler(ITmdbApiClient tmdbApiClient)
    {
        _tmDbApiClient = tmdbApiClient;
    }

    async Task<IEnumerable<MovieResult>> IRequestHandler<GetMoviesQuery, IEnumerable<MovieResult>>.Handle(GetMoviesQuery request, CancellationToken cancellationToken)
    {
        var result = await _tmDbApiClient.GetMoviesAsync(request.Expression);
        return result.Select(x => new MovieResult(x.Id, x.Title, x.Overview));
    }
}
