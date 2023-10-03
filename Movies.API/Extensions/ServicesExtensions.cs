using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Movies.API.Requests;
using Movies.API.Validators;
using Movies.Application.Handlers;
using Movies.Domain.Repositories;
using Movies.Infrastructure.ApiClient;
using Movies.Infrastructure.Db;
using Movies.Infrastructure.Handlers.Queries;
using Movies.Infrastructure.Repositories;
using Movies.Infrastructure.Settings;

namespace Movies.API.Extensions;

/// <summary>
/// Extension is responsible for configuring DI.
/// </summary>
public static class ServiceCollectionConfigurator
{
    /// <summary>
    /// Use this method to add additional DI configurations.
    /// </summary>
    public static void Configure(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var apiOptions = new ApiSettings();
        configuration.GetSection(ApiSettings.KeyName).Bind(apiOptions);
        serviceCollection.Configure<ApiSettings>(configuration.GetSection(ApiSettings.KeyName));
        serviceCollection.AddDbContext<MoviesDbContext>(opt => opt.UseInMemoryDatabase(configuration["DbName"]));
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddMediatR(typeof(AddMovieToUserWatchlistCommandHandler).Assembly, typeof(GetMoviesQueryHandler).Assembly);

        // Domain/Application/Infrastructure configuration injections
        serviceCollection.AddSingleton<ITmdbApiClient, TmdbApiClient>();
        serviceCollection.AddScoped<IMoviesRepository, MoviesRepository>();
        serviceCollection.AddScoped<IUserRepository, UserRepository>();

        // HTTPClient Configuration
        serviceCollection.AddHttpClient(apiOptions.Name, opt =>
                    {
                        opt.BaseAddress = new Uri(apiOptions.BaseUrl);
                        opt.DefaultRequestHeaders.Clear();
                        opt.DefaultRequestHeaders.Add("Accept", "application/json");
                        opt.DefaultRequestHeaders.Add("Authorization", apiOptions.ApiKey);
                    });

        // Validation injections
        serviceCollection.AddScoped<IValidator<AddMovieToUserWatchlistRequest>, AddMovieToUserWatchlistRequestValidator>();
        serviceCollection.AddScoped<IValidator<UpdateMovieWatchedRequest>, UpdateMovieWatchedRequestValidator>();

        serviceCollection.AddSwaggerGen(opt =>
        {
            opt.EnableAnnotations();

            opt.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "SpaceNet API",
                Version = "V1",
                Description = "Minimal API that interacts with TMDB, fetches results in memory db and allows user to add/remove/mark movies in watchlist.",
            });
        });
    }
}
