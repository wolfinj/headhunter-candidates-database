using System.ComponentModel.DataAnnotations;

namespace CatchSmartHeadHunter;

public class Position : Entity
{
    [Required]
    public string PositionName { get; set; }

    public string? Description { get; set; }
}
