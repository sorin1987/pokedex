using System.Text.Json.Serialization;

namespace PokeDex.Api.Domain.Models.Pokemon
{
    public class Language
    {
        [JsonPropertyName("name")] public string Name { get; set; }
    }
}