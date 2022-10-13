namespace CatchSmartHeadHunter.RequestModels;

public class CompanyRequest
{
    public string Name { get; set; }
    public string Email { get; set; }

    public string? Description { get; set; }
    public PositionRequest[] OpenPositions { get; set; }
}
