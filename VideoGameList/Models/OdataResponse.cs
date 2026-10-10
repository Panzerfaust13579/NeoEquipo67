using System.Text.Json.Serialization;

namespace VideoGameList.Models;

public class ODataResponse<T>
{
    [JsonPropertyName("@odata.context")]
    public string? Context { get; set; }

    [JsonPropertyName("@odata.count")]
    public long? Count { get; set; }

    [JsonPropertyName("value")]
    public List<T> Value { get; set; } = new();
}
