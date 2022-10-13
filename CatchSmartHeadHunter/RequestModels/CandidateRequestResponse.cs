using System.Text.Json.Serialization;

namespace CatchSmartHeadHunter.RequestModels;

public class CandidateRequestResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Id { get; set; }

    public string FullName { get; set; }
    public string Email { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? About { get; set; }

    public string[] Skills { get; set; }
    public PositionRequestResponse[] ApliedPositions { get; set; }
}
