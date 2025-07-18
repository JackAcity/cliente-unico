using System.Text.Json.Serialization;

namespace Sica.Application.Dto
{
    public class RestrictionReasonDto
    {
       [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("label")]
        public string Label { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
