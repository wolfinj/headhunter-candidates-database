using System.ComponentModel.DataAnnotations;

namespace CatchSmartHeadHunter.Core.Models;

public class Position : Entity
{
    [Required]
    public string PositionName { get; set; }

    public string? Description { get; set; }
}
