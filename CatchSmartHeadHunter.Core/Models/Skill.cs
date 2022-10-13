using System.ComponentModel.DataAnnotations;

namespace CatchSmartHeadHunter.Core.Models;

public class Skill : Entity
{
    [Required]
    public string SkillName { get; set; }

    public Skill(string skillName)
    {
        SkillName = skillName;
    }
}
