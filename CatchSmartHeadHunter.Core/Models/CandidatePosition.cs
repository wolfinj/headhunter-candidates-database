namespace CatchSmartHeadHunter.Core.Models;

public class CandidatePosition : Entity
{
    public Position Position { get; set; }

    public CandidatePosition()
    {
    }

    public CandidatePosition(Position position)
    {
        Position = position;
    }
}
