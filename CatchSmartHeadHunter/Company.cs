namespace CatchSmartHeadHunter;

public class Company : Entity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string? Description { get; set; }
    public ICollection<CompanyPosition>? OpenPositions { get; set; }

    public Company()
    {
        OpenPositions = new List<CompanyPosition>();
    }
}
