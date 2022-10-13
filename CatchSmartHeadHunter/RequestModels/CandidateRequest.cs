using System.Text.Json.Serialization;

namespace CatchSmartHeadHunter.RequestModels;

public class CandidateRequest
{
    public string FullName { get; set; }
    public string Email { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? About { get; set; }
    public string[] Skills { get; set; }
}
