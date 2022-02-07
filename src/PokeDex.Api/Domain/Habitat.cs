using System.Text.Json.Serialization;

namespace PokeDex.Api.Domain
{
    public class Habitat
    {
        [JsonPropertyName("name")] 
        public string Name { get; set; }
    }
}