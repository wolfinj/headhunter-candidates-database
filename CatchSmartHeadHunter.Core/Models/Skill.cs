using System.ComponentModel.DataAnnotations;

namespace CatchSmartHeadHunter.Core.Models;

public class Skill : Entity
{
    [Required]
    public string SkillName { get; set; }
}
