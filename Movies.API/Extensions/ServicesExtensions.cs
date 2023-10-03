using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Movies.Application.Handlers;
using Movies.Domain.Repositories;
using Movies.Infrastructure.ApiClient;
using Movies.Infrastructure.Db;
using Movies.Infrastructure.Handlers.Queries;
using Movies.Infrastructure.Repositories;
using Movies.Infrastructure.Settings;

namespace Movies.API.Extensions;

public static class ServiceCollectionConfigurator
{
    public static void Configure(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var apiOptions = new ApiSettings();
        configuration.GetSection(ApiSettings.KeyName).Bind(apiOptions);
        serviceCollection.Configure<ApiSettings>(configuration.GetSection(ApiSettings.KeyName));
        serviceCollection.AddDbContext<MoviesDbContext>(opt => opt.UseInMemoryDatabase(configuration["DbName"]));
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddMediatR(typeof(AddMovieToUserWatchlistCommandHandler).Assembly, typeof(GetMoviesQueryHandler).Assembly);

        serviceCollection.AddSingleton<ITmdbApiClient, TmdbApiClient>();
        serviceCollection.AddScoped<IMoviesRepository, MoviesRepository>();
        serviceCollection.AddScoped<IUserRepository, UserRepository>();

        serviceCollection.AddHttpClient(apiOptions.Name, opt =>
                    {
                        opt.BaseAddress = new Uri(apiOptions.BaseUrl);
                        opt.DefaultRequestHeaders.Clear();
                        opt.DefaultRequestHeaders.Add("Accept", "application/json");
                        opt.DefaultRequestHeaders.Add("Authorization", apiOptions.ApiKey);
                    });

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
