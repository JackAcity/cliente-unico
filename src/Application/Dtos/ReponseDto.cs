using System.Text.Json.Serialization;

namespace Sica.Application.Dto;

public class ResponseDto<T>
{
    [JsonPropertyName("ok")]
    public bool Ok { get; set; } = false;

    [JsonPropertyName("data")]
    public T? Data { get; set; } = default;

    [JsonPropertyName("statusCode")]
    public int StatusCode { get; set; } = 500;

    [JsonPropertyName("message")]
    public string Message { get; set; } = "";
}
