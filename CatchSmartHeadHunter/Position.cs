using System.ComponentModel.DataAnnotations;

namespace CatchSmartHeadHunter;

public class Position : Entity
{
    [Required]
    public string PositionName { get; set; }

    public string? Description { get; set; }
    // public ICollection<ReqSkill> RequiredSkills { get; set; }
    //
    // public Position()
    // {
    //     RequiredSkills = new List<ReqSkill>();
    // }
}
