namespace CatchSmartHeadHunter.Core.Exceptions;

public class OpenPositionNotAvaibleException : Exception
{
    public OpenPositionNotAvaibleException() : base("Position not available.")
    {
    }

    public OpenPositionNotAvaibleException(int id) : base($"Position with id:\"{id}\" not available.")
    {
    }

    public OpenPositionNotAvaibleException(string message)
        : base(message)
    {
    }

    public OpenPositionNotAvaibleException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
