using MediatR;
using Microsoft.OpenApi.Models;
using Movies.Application.Queries;
using Movies.Infrastructure.ApiClient;
using Movies.Infrastructure.Settings;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var apiOptions = new ApiSettings();
builder.Configuration.GetSection("ApiSettings").Bind(apiOptions);
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection(ApiSettings.KeyName));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddSingleton<ITmdbApiClient, TmdbApiClient>();

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

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/movies/{expression}", async (string expression, IMediator mediator) =>
{
    var query = new GetMoviesQuery(expression);
    var result = await mediator.Send(query);
    return Results.Ok(result);
});


// Add movie to the watchlist
app.MapPost("/user/{userId:int}/watchlist", async () =>
{

});

app.Run();


public class Objectus
{
    [JsonPropertyName("results")]
    public List<Mov> Movies { get; set; }
}
public class Mov
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Overview { get; set; }
}