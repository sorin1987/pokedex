using System.Text.Json.Serialization;

namespace PokeDex.Api.Domain
{
    public class Language
    {
        [JsonPropertyName("name")] 
        public string Name { get; set; }
    }
}