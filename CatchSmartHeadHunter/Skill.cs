using System.ComponentModel.DataAnnotations;

namespace CatchSmartHeadHunter;

public class Skill : Entity
{
    [Required]
    public string SkillName { get; set; }
}
