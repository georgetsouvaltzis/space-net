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

namespace Movies.API.Extensions
{
    public static class ServiceCollectionConfigurator
    {
        public static void Configure(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var apiOptions = new ApiSettings();
            configuration.GetSection("ApiSettings").Bind(apiOptions);
            serviceCollection.Configure<ApiSettings>(configuration.GetSection(ApiSettings.KeyName));
            serviceCollection.AddDbContext<MoviesDbContext>(opt => opt.UseInMemoryDatabase("MoviesDb"));
            serviceCollection.AddEndpointsApiExplorer();
            serviceCollection.AddMediatR(typeof(AddMovieToUserWatchlistCommandHandler).Assembly, typeof(GetMoviesQueryHandler).Assembly);
            serviceCollection.AddSingleton<ITmdbApiClient, TmdbApiClient>();
            serviceCollection.AddScoped<IMoviesRepository, MoviesRepository>();
            serviceCollection.AddScoped<IUserRepository, UserRepository>();
            serviceCollection.AddScoped<IWatchListRepository, WatchListRepository>();
            serviceCollection.AddHttpClient(apiOptions.Name, opt =>
                        {
                            opt.BaseAddress = new Uri(apiOptions.BaseUrl);
                            opt.DefaultRequestHeaders.Clear();
                            opt.DefaultRequestHeaders.Add("Accept", "application/json");
                            opt.DefaultRequestHeaders.Add("Authorization", apiOptions.ApiKey);
                        });

            serviceCollection.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "SpaceNet API",
                    Version = "V1",
                    Description = "Simple Minimal API to interact with IMDB to fetch results",
                });
            });
        }
    }
}
