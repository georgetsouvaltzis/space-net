﻿using MediatR;
using Movies.API.Requests;
using Movies.API.Responses;
using Movies.Application.Commands;
using Movies.Application.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Movies.API.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static void MapMoviesEndpoints(this WebApplication app)
    {
        app.MapGet("/movies/{expression}", async (string expression, IMediator mediator) =>
        {
            var query = new GetMoviesQuery(expression);
            var result = await mediator.Send(query);

            var command = new AddMovieToPersistenceCommand(result);

            _ = await mediator.Send(command);
            return Results.Ok(result.Select(x => new MovieResponse
            {
                Id = x.Id,
                Title = x.Title,
                Overview = x.Overview,
            }));
        }).Produces<IEnumerable<MovieResponse>>()
          .WithMetadata(new SwaggerOperationAttribute("Fetch results from TMDB API", "Adds results in inmemory db after fetching the results for future operations."));
    }

    public static void MapUserWatchlistEndpoints(this WebApplication app)
    {
        app.MapPost("/user/{userId:int}/watchlist", async (int userId, AddMovieToUserWatchlistRequest request, IMediator mediator) =>
        {
            var command = new AddMovieToUserWatchlistCommand(userId, request.MovieId);

            var result = await mediator.Send(command);
            return Results.Ok(); // Ideally we would like to have Results.CreatedAt(....)
        }).Produces(StatusCodes.Status200OK, typeof(void))
           .WithMetadata(new SwaggerOperationAttribute("Adds movie into user's watchlist", "Movie ID is the one that user gets from /movies/{expression} endpoint."));

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
        }).Produces<IEnumerable<UserWatchlistMoviesResponse>>()
        .WithMetadata(new SwaggerOperationAttribute("User Watchlisted movies", "Provides information regarding user's watchlisted movies and whether they have watched it or not."));

        app.MapMethods("/user/{userId:int}/watchlist", new[] { HttpMethod.Patch.Method }, async (int userId, UpdateMovieWatchedRequest request, IMediator mediator) =>
        {
            var command = new UpdateMovieWatchedCommand(userId, request.MovieId);

            var result = await mediator.Send(command);
            return Results.NoContent();

        }).Produces(StatusCodes.Status204NoContent, typeof(void))
        .WithMetadata(new SwaggerOperationAttribute("Mark as watched", "Marking movie as Watched by setting IsWatched = true"));
    }
}
