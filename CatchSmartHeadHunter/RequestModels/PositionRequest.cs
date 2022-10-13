using System.Text.Json.Serialization;

namespace CatchSmartHeadHunter.RequestModels;

public class PositionRequest
{
    public string PositionName { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; set; }
}
