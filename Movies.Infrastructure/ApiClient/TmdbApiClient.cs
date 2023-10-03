using Microsoft.Extensions.Options;
using Movies.Infrastructure.DTOs;
using Movies.Infrastructure.Settings;
using System.Net.Http.Json;

namespace Movies.Infrastructure.ApiClient;

public interface ITmdbApiClient
{
    Task<IEnumerable<MovieDTO>> GetMoviesAsync(string expression);
}

public class TmdbApiClient : ITmdbApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string QueryPrefix = "?query=";

    public TmdbApiClient(IHttpClientFactory httpClientFactory, IOptions<ApiSettings> settings )
    {
        _httpClient = httpClientFactory.CreateClient(settings.Value.Name);
    }

    public async Task<IEnumerable<MovieDTO>> GetMoviesAsync(string expression)
    {
        if (string.IsNullOrEmpty(expression)) throw new ArgumentNullException(expression);

        var result = await _httpClient.GetFromJsonAsync<WrapperDTO>($"{QueryPrefix}{expression}");
        return result.Movies;
    }
}
