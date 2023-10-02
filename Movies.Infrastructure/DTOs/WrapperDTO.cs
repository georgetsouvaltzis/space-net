using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Movies.Infrastructure.DTOs
{
    internal class WrapperDTO
    {
        [JsonPropertyName("results")]
        public IEnumerable<MovieDTO> Movies { get; set; }
    }
}
