using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PokeDex.Api.Domain.Pokemon
{
    public class Pokemon
    {
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("habitat")] public Habitat Habitat { get; set; }
        [JsonPropertyName("is_legendary")] public bool IsLegendary { get; set; }
        [JsonPropertyName("flavor_text_entries")] public IEnumerable<Description> Descriptions { get; set; }
    }
}