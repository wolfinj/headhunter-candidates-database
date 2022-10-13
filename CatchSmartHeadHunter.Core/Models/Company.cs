using System.ComponentModel.DataAnnotations;

namespace CatchSmartHeadHunter.Core.Models;

public class Company : Entity
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    public string? Description { get; set; }
    public ICollection<CompanyPosition>? OpenPositions { get; set; }

    public Company()
    {
        OpenPositions = new List<CompanyPosition>();
    }
}
