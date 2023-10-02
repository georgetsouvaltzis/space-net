using MediatR;
using Movies.Application.Commands;
using Movies.Domain.Repositories;

namespace Movies.Application.Handlers;

public class AddMovieToUserWatchlistCommandHandler : IRequestHandler<AddMovieToUserWatchlistCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IMoviesRepository _moviesRepository;

    public AddMovieToUserWatchlistCommandHandler(IUserRepository userRepository, IMoviesRepository moviesRepository)
    {
        _userRepository = userRepository;
        _moviesRepository = moviesRepository;
    }

    public async Task<Unit> Handle(AddMovieToUserWatchlistCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetUserAsync(request.UserId);
        var existingMovie = await _moviesRepository.GetAsync(request.MovieId);

        existingMovie.WatchListId = existingUser.WatchList.Id;

        await _moviesRepository.UpdateAsync(existingMovie);
        return Unit.Value;
    }
}
