using MediatR;
using Movies.API.Extensions;
using Movies.API.Requests;
using Movies.API.Responses;
using Movies.Application.Commands;
using Movies.Application.Queries;
using Movies.Infrastructure.Seeder;

var builder = WebApplication.CreateBuilder(args);
ServiceCollectionConfigurator.Configure(builder.Services, builder.Configuration);

var app = builder.Build();
await DatabaseInitializer.InitializeAsync(app.Services);

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/movies/{expression}", async (string expression, IMediator mediator) =>
{
    var query = new GetMoviesQuery(expression);
    var result = await mediator.Send(query);
    return Results.Ok(result);
});


app.MapPost("/user/{userId:int}/watchlist", async (int userId, AddMovieToUserWatchlistRequest request, IMediator mediator) =>
{
    var command = new AddMovieToUserWatchlistCommand(userId, request.MovieId);

    var result = await mediator.Send(command);
    return Results.Ok(); // Ideally we would like to have Results.CreatedAt(....)
});

app.MapGet("/user/{userId:int}/watchlist", async (int userId, IMediator mediator) =>
{
    var command = new GetUserWatchlistMoviesQuery(userId);

    var result = await mediator.Send(command);

    return Results.Ok(result.Select(x => new UserWatchlistMoviesResponse
    {
        Id = x.Id,
        Overview = x.Overview,
        Title = x.Title,
        IsWatched = x.IsWatched
    }));
});

app.MapPut("/user/{userId:int}/watchlist", async (int userId, UpdateMovieWatchedRequest request, IMediator mediator) =>
{
    var command = new UpdateMovieWatchedCommand(userId, request.MovieId);

    var result = await mediator.Send(command);
    return Results.Ok();

});

app.Run();
