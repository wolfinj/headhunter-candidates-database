namespace CatchSmartHeadHunter.Core.Exceptions;

public class CompanyNotFoundException : Exception
{
    public CompanyNotFoundException() :base("Company not found.")
    {
    }

    public CompanyNotFoundException(int id) :base($"Company with id:\"{id}\" not found.")
    {
        
    }

    public CompanyNotFoundException(string message)
        : base(message)
    {
    }

    public CompanyNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
