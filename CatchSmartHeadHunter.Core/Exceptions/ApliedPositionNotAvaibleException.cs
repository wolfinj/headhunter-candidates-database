namespace CatchSmartHeadHunter.Core.Exceptions;

public class ApliedPositionNotAvaibleException : Exception
{
    public ApliedPositionNotAvaibleException() : base("Position not available.")
    {
    }

    public ApliedPositionNotAvaibleException(int id) : base($"Position with id:\"{id}\" not available.")
    {
    }

    public ApliedPositionNotAvaibleException(string message)
        : base(message)
    {
    }

    public ApliedPositionNotAvaibleException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
