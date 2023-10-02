using MediatR;

namespace Movies.Application.Commands;

public record class AddMovieToUserWatchlistCommand(int UserId, int MovieId) : IRequest;
