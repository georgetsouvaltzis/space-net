using MediatR;

namespace Movies.Application.Commands;

public record class UpdateMovieWatchedCommand(int UserId, int MovieId) : IRequest;