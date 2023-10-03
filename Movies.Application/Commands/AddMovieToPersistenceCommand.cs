using MediatR;
using Movies.Application.Results;

namespace Movies.Application.Commands;

public record class AddMovieToPersistenceCommand(IEnumerable<MovieResult> MovieResults) : IRequest;
