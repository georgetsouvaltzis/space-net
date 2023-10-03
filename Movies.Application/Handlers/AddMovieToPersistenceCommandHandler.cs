using MediatR;
using Movies.Application.Commands;
using Movies.Domain.Entities;
using Movies.Domain.Repositories;

namespace Movies.Application.Handlers;

public class AddMovieToPersistenceCommandHandler : IRequestHandler<AddMovieToPersistenceCommand>
{
    private readonly IMoviesRepository _moviesRepository;
    public AddMovieToPersistenceCommandHandler(IMoviesRepository moviesRepository)
    {
        _moviesRepository = moviesRepository;
    }
    
    public async Task<Unit> Handle(AddMovieToPersistenceCommand request, CancellationToken cancellationToken)
    {
        await _moviesRepository.AddBulkAsync(request.MovieResults.Select(x => new Movie
        {
            Id = x.Id,
            Overview = x.Overview,
            Title = x.Title,
        }).ToList());

        return Unit.Value;
    }
}
