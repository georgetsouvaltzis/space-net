using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var apiOptions = new ApiOptions();
builder.Configuration.GetSection("ApiOptions").Bind(apiOptions);

builder.Services.AddEndpointsApiExplorer();

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

app.MapGet("/movies/{expression}", async (string expression, IHttpClientFactory httpClientFactory) =>
{
    var client = httpClientFactory.CreateClient("TMDB");
    var mov = await client.GetFromJsonAsync<Objectus>($"?query={expression}");
});


// Add movie to the watchlist
app.MapPost("/user/{userId:int}/watchlist", async () =>
{

});

app.Run();

public class ApiOptions
{
    public string Name { get; set; }
    public string ApiKey { get; set; }
    public string BaseUrl { get; set; }
}

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