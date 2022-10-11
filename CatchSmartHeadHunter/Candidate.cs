using System.ComponentModel.DataAnnotations;

namespace CatchSmartHeadHunter;

public class Candidate : Entity
{
    [Required]
    public string FullName { get; set; }
    [Required]
    public string Email { get; set; }
    public string? About { get; set; }
    public ICollection<Skill> Skills { get; set; }
    public ICollection<CandidatePosition>? AppliedPositions { get; set; }
    // public List<CandidateSkill>CandidateSkills { get; set; }

    public Candidate()
    {
        Skills = new List<Skill>();
        AppliedPositions = new List<CandidatePosition>();
    }
}
