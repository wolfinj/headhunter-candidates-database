namespace CatchSmartHeadHunter.RequestModels;

public class CandidateRequest
{
    public string FullName { get; set; }
    public string Email { get; set; }

    public string? About { get; set; }

    public string[] Skills { get; set; }
}
