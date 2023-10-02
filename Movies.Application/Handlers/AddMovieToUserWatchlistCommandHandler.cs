using MediatR;
using Movies.Application.Commands;
using Movies.Domain.Repositories;

namespace Movies.Application.Handlers;

public class AddMovieToUserWatchlistCommandHandler : IRequestHandler<AddMovieToUserWatchlistCommand>
{
    private readonly IUserRepository _userRepository;
    public AddMovieToUserWatchlistCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Unit> Handle(AddMovieToUserWatchlistCommand request, CancellationToken cancellationToken)
    {
        await _userRepository.AddToWatchlistAsync(request.UserId, request.MovieId);
        return Unit.Value;
    }
}
