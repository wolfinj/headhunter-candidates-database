using CatchSmartHeadHunter.Core.Exceptions;
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
        var candidate = Context.Candidates
            .Include(c => c.AppliedPositions)
            .ThenInclude(op => op.Position)
            .Include(c => c.Skills)
            .SingleOrDefault(c => c.Id == id);
        
        if (candidate == null)
        {
            throw new CandidateNotFoundException(id);
        }

        return candidate;
    }

    public ICollection<Candidate> GetCompleteCandidates()
    {
        return Context.Candidates
            .Include(c => c.AppliedPositions)
            .ThenInclude(op => op.Position)
            .Include(c => c.Skills)
            .ToList();
    }

    public void AddPositionToAppliedPositions(Candidate candidate, Position position)
    {
        var candidatePosition = new CandidatePosition(position);
        Context.Add(position);
        candidate.AppliedPositions!.Add(candidatePosition);
        Context.SaveChanges();
    }

    public void RemovePositionFromCandidateById(int candidateId, int positionId)
    {
        var candidate = Context.Candidates.SingleOrDefault(c => c.Id == candidateId);

        if (candidate == null)
        {
            throw new CandidateNotFoundException(candidateId);
        }

        var candidatePosition = (candidate.AppliedPositions ?? throw new ApliedPositionNotAvaibleException(positionId))
            .SingleOrDefault(cp => cp.Position.Id == positionId);

        if (candidatePosition == null)
        {
            throw new ApliedPositionNotAvaibleException(positionId);
        }

        Context.CandidatePositions.Remove(candidatePosition);

        Context.SaveChanges();
    }

    public void AddPositionToAppliedPositionsById(int candidateId, int positionId)
    {
        var candidate = Context.Candidates.SingleOrDefault(c => c.Id == candidateId);

        if (candidate == null)
        {
            throw new CandidateNotFoundException(candidateId);
        }

        var position = Context.Positions.SingleOrDefault(p => p.Id == positionId);

        if (position == null)
        {
            throw new PositionNotFoundException(positionId);
        }

        var candidatePosition = new CandidatePosition(position);

        candidate.AppliedPositions!.Add(candidatePosition);
        Context.SaveChanges();
    }

    public ICollection<Company> GetCompanies(int id)
    {
        var candidate = GetCompleteCandidateById(id);

        var positionIds = candidate.AppliedPositions.Select(ap => ap.Position.Id);

        var companiesPositions = Context.CompanyPositions
            .Where(c => positionIds.Contains(c.Position.Id))
            .Select(cp => cp.Id);

        var companies = Context.Companies
            .Include(c => c.OpenPositions)
            .ThenInclude(cp => cp.Position)
            .Where(c => c.OpenPositions.Any(op => companiesPositions.Contains(op.Id)));

        return companies.ToList();
    }
}
