using System.Text.Json.Serialization;

namespace PokeDex.Api.Domain.Translation
{
    public class Content
    {
        [JsonPropertyName("translated")]
        public string Translated { get; set; }
    }
}