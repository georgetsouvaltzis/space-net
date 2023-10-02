using MediatR;
using Movies.Application.Commands;
using Movies.Domain.Repositories;

namespace Movies.Application.Handlers;

public class UpdateMovieWatchedCommandHandler : IRequestHandler<UpdateMovieWatchedCommand>
{
    private readonly IMoviesRepository _moviesRepository;
    public UpdateMovieWatchedCommandHandler(IMoviesRepository moviesRepository)
    {
        _moviesRepository = moviesRepository;
    }
    public async Task<Unit> Handle(UpdateMovieWatchedCommand request, CancellationToken cancellationToken)
    {
        var existingMovie = await _moviesRepository.GetAsync(request.MovieId);

        existingMovie.IsWatched = true;
        await _moviesRepository.UpdateAsync(existingMovie);

        return Unit.Value;
    }
}
