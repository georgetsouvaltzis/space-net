using MediatR;
using Movies.Application.Commands;
using Movies.Domain.Repositories;

namespace Movies.Application.Handlers;

public class UpdateMovieWatchedCommandHandler : IRequestHandler<UpdateMovieWatchedCommand>
{
    private readonly IUserRepository _userRepository;
    public UpdateMovieWatchedCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Unit> Handle(UpdateMovieWatchedCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.UpdateWatchlistMovieToWatched(request.UserId, request.MovieId);

        return Unit.Value;
    }
}
