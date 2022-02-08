using System.Text.Json.Serialization;

namespace PokeDex.Api.Domain.Pokemon
{
    public class Description
    {
        [JsonPropertyName("flavor_text")] public string Text { get; set; }

        [JsonPropertyName("language")] public Language Language { get; set; }
    }
}