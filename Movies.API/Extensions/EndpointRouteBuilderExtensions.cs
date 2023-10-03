using FluentValidation;
using MediatR;
using Movies.API.Requests;
using Movies.API.Responses;
using Movies.Application.Commands;
using Movies.Application.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace Movies.API.Extensions;

/// <summary>
/// Extension is responsible for mapping Routes.
/// </summary>
public static class EndpointRouteBuilderExtensions
{
    /// <summary>
    /// Maps Movies endpoint to hosted <see cref="WebApplication"/>.
    /// </summary>
    public static void MapMoviesEndpoints(this WebApplication app)
    {
        app.MapGet("/movies/{expression}", async (string expression, IMediator mediator) =>
        {
            // This way validation is possible as well for Route params.
            if (string.IsNullOrEmpty(expression))
                return Results.BadRequest("expression can't be null or empty.");

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
        })
            .Produces<IEnumerable<MovieResponse>>()
            .Produces(StatusCodes.Status400BadRequest, typeof(string))
            .WithMetadata(new SwaggerOperationAttribute("Fetch results from TMDB API", "Adds results in inmemory db after fetching the results for future operations."));
    }

    /// <summary>
    /// Maps User endpoint to hosted <see cref="WebApplication"/>.
    /// </summary>
    public static void MapUserWatchlistEndpoints(this WebApplication app)
    {
        app.MapPost("/user/{userId:int}/watchlist",
            async (int userId,
            AddMovieToUserWatchlistRequest request,
            IMediator mediator,
            IValidator<AddMovieToUserWatchlistRequest> validator) =>
            {
                var validationResult = await validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                    return Results.ValidationProblem(validationResult.ToDictionary());

                var command = new AddMovieToUserWatchlistCommand(userId, request.MovieId);

                var result = await mediator.Send(command);
                return Results.Ok(); // Ideally we would like to have Results.CreatedAt(....)
            })
            .Produces(StatusCodes.Status200OK, typeof(void))
            .ProducesValidationProblem(StatusCodes.Status400BadRequest)
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
        })
            .Produces<IEnumerable<UserWatchlistMoviesResponse>>()
            .ProducesValidationProblem(StatusCodes.Status400BadRequest)
            .WithMetadata(new SwaggerOperationAttribute("User Watchlisted movies", "Provides information regarding user's watchlisted movies and whether they have watched it or not."));

        app.MapMethods("/user/{userId:int}/watchlist",
            new[] { HttpMethod.Patch.Method },
            async (int userId, UpdateMovieWatchedRequest request, IMediator mediator,
            IValidator<UpdateMovieWatchedRequest> validator) =>
            {
                var validationResult = await validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                    return Results.ValidationProblem(validationResult.ToDictionary());

                var command = new UpdateMovieWatchedCommand(userId, request.MovieId);

                var result = await mediator.Send(command);
                return Results.NoContent();

            })
            .Produces(StatusCodes.Status204NoContent, typeof(void))
            .ProducesValidationProblem(StatusCodes.Status400BadRequest)
            .WithMetadata(new SwaggerOperationAttribute("Mark as watched", "Marking movie as Watched by setting IsWatched = true"));
    }
}
