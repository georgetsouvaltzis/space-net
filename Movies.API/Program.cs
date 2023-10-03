
using Movies.API.Extensions;
using Movies.Infrastructure.Seeder;

var builder = WebApplication.CreateBuilder(args);
ServiceCollectionConfigurator.Configure(builder.Services, builder.Configuration);
    
var app = builder.Build();
await DatabaseInitializer.InitializeAsync(app.Services);

app.UseSwagger();
app.UseSwaggerUI();

app.MapMoviesEndpoints();
app.MapUserWatchlistEndpoints();

app.Run();
