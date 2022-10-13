namespace CatchSmartHeadHunter.Core.Exceptions;

public class PositionNotFoundException : Exception
{
    public PositionNotFoundException() : base("Position not found.")
    {
    }

    public PositionNotFoundException(int id) : base($"Position with id:\"{id}\" not found.")
    {
    }

    public PositionNotFoundException(string message)
        : base(message)
    {
    }

    public PositionNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
