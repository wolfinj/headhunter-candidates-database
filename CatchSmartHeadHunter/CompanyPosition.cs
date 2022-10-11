namespace CatchSmartHeadHunter;

public class CompanyPosition : Entity
{
    public Position Position { get; set; }

    public CompanyPosition()
    {
    }

    public CompanyPosition(Position position)
    {
        Position = position;
    }
}
