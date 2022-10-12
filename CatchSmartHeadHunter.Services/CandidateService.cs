using CatchSmartHeadHunter.Core.Models;
using CatchSmartHeadHunter.Core.Services;
using CatchSmartHeadHunter.Data;
using Microsoft.EntityFrameworkCore;

namespace CatchSmartHeadHunter.Services;

public class CandidateService : EntityService<Candidate>, ICandidateService
{
    public CandidateService(HhDbContext context) : base(context)
    {
    }

    public Candidate GetCompleteCandidateById(int id)
    {
        return _context.Candidates
            .Include(c => c.AppliedPositions)
            .ThenInclude(op => op.Position)
            .Include(c=>c.Skills)
            .SingleOrDefault(c => c.Id == id);
    }

    public ICollection<Candidate> GetCompleteCandidates()
    {
        return _context.Candidates
            .Include(c => c.AppliedPositions)
            .ThenInclude(op => op.Position)
            .Include(c=>c.Skills)
            .ToList();
    }

    public void AddPositionToAppliedPositions(Candidate candidate, Position position)
    {
        var candidatePosition = new CandidatePosition(position);
        _context.Add(position);
        candidate.AppliedPositions.Add(candidatePosition);
        _context.SaveChanges();
    }

    public void RemovePositionFromCandidateById(int candidateId,int positionID)
    {
        var candidate = _context.Candidates.SingleOrDefault(c => c.Id == candidateId);
        var candidatePosition = candidate.AppliedPositions.SingleOrDefault(cp => cp.Position.Id == positionID);

        _context.CandidatePositions.Remove(candidatePosition);
        
        _context.SaveChanges();
    }

    public void AddPositionToAppliedPositionsById(int candidateId,int positionID)
    {
        Position position = _context.Positions.SingleOrDefault(p => p.Id == positionID);
        CandidatePosition candidatePosition = new CandidatePosition(position);
        Candidate candidate = _context.Candidates.SingleOrDefault(c => c.Id == candidateId);

        candidate.AppliedPositions.Add(candidatePosition);
        _context.SaveChanges();
    }
}
