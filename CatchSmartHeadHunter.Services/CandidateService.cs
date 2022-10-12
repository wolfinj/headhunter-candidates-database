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
            .Include(c => c.Skills)
            .SingleOrDefault(c => c.Id == id);
    }

    public ICollection<Candidate> GetCompleteCandidates()
    {
        return _context.Candidates
            .Include(c => c.AppliedPositions)
            .ThenInclude(op => op.Position)
            .Include(c => c.Skills)
            .ToList();
    }

    public void AddPositionToAppliedPositions(Candidate candidate, Position position)
    {
        var candidatePosition = new CandidatePosition(position);
        _context.Add(position);
        candidate.AppliedPositions.Add(candidatePosition);
        _context.SaveChanges();
    }

    public void RemovePositionFromCandidateById(int candidateId, int positionId)
    {
        var candidate = _context.Candidates.SingleOrDefault(c => c.Id == candidateId);
        var candidatePosition = candidate.AppliedPositions.SingleOrDefault(cp => cp.Position.Id == positionId);

        _context.CandidatePositions.Remove(candidatePosition);

        _context.SaveChanges();
    }

    public void AddPositionToAppliedPositionsById(int candidateId, int positionId)
    {
        Position position = _context.Positions.SingleOrDefault(p => p.Id == positionId);
        CandidatePosition candidatePosition = new CandidatePosition(position);
        Candidate candidate = _context.Candidates.SingleOrDefault(c => c.Id == candidateId);

        candidate.AppliedPositions.Add(candidatePosition);
        _context.SaveChanges();
    }

    public ICollection<Company> GetCompanies(int id)
    {
        var position = _context.Candidates
            .Include(c=>c.AppliedPositions)
            .ThenInclude(cp=>cp.Position)
            .SingleOrDefault(c => c.Id == id);
        var apliedPositions = position.AppliedPositions;
        
        var positionIds=apliedPositions.Select(ap=>ap.Position.Id);
        
        var companiesPositions = _context.CompanyPositions
            .Where(c => positionIds.Contains(c.Position.Id))
            .Select(cp=>cp.Id);
        
        var companies = _context.Companies
            .Include(c=>c.OpenPositions)
            .ThenInclude(cp=>cp.Position)
            .Where(c => c.OpenPositions.Any(op => companiesPositions.Contains(op.Id)));

        var test = true;
        
        return companies.ToList();
    }
}
