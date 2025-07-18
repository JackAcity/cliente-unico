using System.Text.Json.Serialization;

namespace ValidarSica.Dtos
{
    public class DocumentPlayerDto 
    {
         [JsonPropertyName("document")]
        public string Document { get; set; } = "";
    }
}
