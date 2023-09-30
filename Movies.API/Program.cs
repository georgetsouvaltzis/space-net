using IMDbApiLib;
using Microsoft.OpenApi.Models;
using System.Net.Http;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var asdf = builder.Configuration["ApiKey"];
var url = builder.Configuration["BaseUrl"];
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SpaceNet API",
        Version = "V1",
        Description = "Simple Minimal API to interact with IMDB to fetch results",
    });

    //opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.MapGet("/", async (IHttpClientFactory httpClientFactory) =>
{
    var apiLib = new ApiLib("k_pyej54z4");
    var data = await apiLib.SearchMovieAsync("inception 2010");
});
app.Run();
