using System.Text.Json.Serialization;

namespace Movies.Infrastructure.DTOs;

internal class WrapperDTO
{
    [JsonPropertyName("results")]
    public IEnumerable<MovieDTO> Movies { get; set; }
}
