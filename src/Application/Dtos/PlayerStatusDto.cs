using System.Text.Json.Serialization;

namespace Sica.Application.Dto;

public class PlayerStatusDto 
{
     [JsonPropertyName("restricted")]
    public bool Restricted { get; set; } = false;

    [JsonPropertyName("reason")]
    public RestrictionReasonDto? Reason { get; set; } = null;
}
