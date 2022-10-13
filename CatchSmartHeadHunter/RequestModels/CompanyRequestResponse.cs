using System.Text.Json.Serialization;

namespace CatchSmartHeadHunter.RequestModels;

public class CompanyRequestResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Id { get; set; }

    public string Name { get; set; }
    public string Email { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Description { get; set; }

    public PositionRequestResponse[] OpenPositions { get; set; }
}
