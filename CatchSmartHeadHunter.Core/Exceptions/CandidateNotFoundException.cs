namespace CatchSmartHeadHunter.Core.Exceptions;

public class CandidateNotFoundException : Exception
{
    public CandidateNotFoundException() : base("Candidate not found.")
    {
    }

    public CandidateNotFoundException(int id) : base($"Candidate with id:\"{id}\" not found.")
    {
    }

    public CandidateNotFoundException(string message)
        : base(message)
    {
    }

    public CandidateNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
