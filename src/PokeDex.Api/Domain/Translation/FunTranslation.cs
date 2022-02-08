using System.Text.Json.Serialization;

namespace PokeDex.Api.Domain.Translation
{
    public class FunTranslation
    {
        [JsonPropertyName("contents")]
        public Content Content { get; set; }
    }
}