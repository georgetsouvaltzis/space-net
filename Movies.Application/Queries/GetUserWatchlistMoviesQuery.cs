using MediatR;
using Movies.Application.Results;

namespace Movies.Application.Queries;

public record class GetUserWatchlistMoviesQuery(int UserId) : IRequest<IEnumerable<WatchlistMovieResult>>;
