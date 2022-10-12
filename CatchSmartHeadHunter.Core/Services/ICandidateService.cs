using CatchSmartHeadHunter.Core.Models;

namespace CatchSmartHeadHunter.Core.Services;

public interface ICandidateService : IEntityService<Candidate>
{
    Candidate GetCompleteCandidateById(int id);
    ICollection<Candidate> GetCompleteCandidates();
    void AddPositionToAppliedPositions(Candidate candidate, Position position);
    void RemovePositionFromCandidateById(int candidateId,int positionID);
    void AddPositionToAppliedPositionsById(int candidateId, int positionID);
    ICollection<Company> GetCompanies(int id);
}
