using System.Text.Json.Serialization;

namespace PokeDex.Api.Domain.Models.Translation
{
    public class FunTranslation
    {
        [JsonPropertyName("contents")]
        public Content Content { get; set; }
    }
}