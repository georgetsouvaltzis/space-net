using MediatR;
using Movies.Application.Results;

namespace Movies.Application.Queries;

public record class GetMoviesQuery(string Expression) : IRequest<IEnumerable<MovieResult>>;

