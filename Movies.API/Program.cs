using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Movies.API.Requests;
using Movies.Application.Commands;
using Movies.Application.Handlers;
using Movies.Application.Queries;
using Movies.Domain.Repositories;
using Movies.Infrastructure.ApiClient;
using Movies.Infrastructure.Db;
using Movies.Infrastructure.Handlers.Queries;
using Movies.Infrastructure.Repositories;
using Movies.Infrastructure.Seeder;
using Movies.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);
var apiOptions = new ApiSettings();
builder.Configuration.GetSection("ApiSettings").Bind(apiOptions);
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection(ApiSettings.KeyName));

builder.Services.AddDbContext<MoviesDbContext>(opt => opt.UseInMemoryDatabase("MoviesDb"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediatR(typeof(AddMovieToUserWatchlistCommandHandler).Assembly, typeof(GetMoviesQueryHandler).Assembly);
builder.Services.AddSingleton<ITmdbApiClient, TmdbApiClient>();
builder.Services.AddScoped<IMoviesRepository, MoviesRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddHttpClient(apiOptions.Name,opt =>
{
    opt.BaseAddress = new Uri(apiOptions.BaseUrl);
    opt.DefaultRequestHeaders.Clear();
    opt.DefaultRequestHeaders.Add("Accept", "application/json");
    opt.DefaultRequestHeaders.Add("Authorization", apiOptions.ApiKey);
});

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SpaceNet API",
        Version = "V1",
        Description = "Simple Minimal API to interact with IMDB to fetch results",
    });
});


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
    var command = new AddMovieToUserWatchlistCommand
    {
        UserId = userId,
        MovieId = request.MovieId
    };

    var result = await mediator.Send(command);
    return Results.Ok();
});

app.MapGet("/user/{userId:int}/watchlist", async (int userId, IMediator mediator) =>
{
    var command = new GetUserWatchlistMoviesQuery
    {
        UserId = userId,
    };
});

app.MapPut("/user/{userId:int}/watchlist", async (int userId, UpdateMovieWatchedRequest request, IMediator mediator) =>
{
    var command = new UpdateMovieWatchedCommand
    {
        UserId = userId,
        MovieId = request.MovieId
    };
    var result = await mediator.Send(command);
    return Results.Ok();
    
});
app.Run();
