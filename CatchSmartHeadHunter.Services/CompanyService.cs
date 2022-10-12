using CatchSmartHeadHunter.Core.Models;
using CatchSmartHeadHunter.Core.Services;
using CatchSmartHeadHunter.Data;
using Microsoft.EntityFrameworkCore;

namespace CatchSmartHeadHunter.Services;

public class CompanyService : EntityService<Company>, ICompanyService
{
    public CompanyService(HhDbContext context) : base(context)
    {
    }

    public Company GetCompleteCompanyById(int id)
    {
        return _context.Companies
            .Include(c => c.OpenPositions)
            .ThenInclude(op => op.Position)
            .SingleOrDefault(c => c.Id == id);
    }

    public ICollection<Company> GetCompleteCompanies()
    {
        return _context.Companies
            .Include(c => c.OpenPositions)
            .ThenInclude(op => op.Position)
            .ToList();
    }

    public void AddPositionToOpenPositions(Company company, Position position)
    {
        var companyPosition = new CompanyPosition(position);
        _context.Add(position);
        company.OpenPositions.Add(companyPosition);
        _context.SaveChanges();
    }

    public void RemovePositionFromCompanyById(int companyId,int positionID)
    {
        var company = _context.Companies.SingleOrDefault(c => c.Id == companyId);
        var companyPosition = company.OpenPositions.SingleOrDefault(cp => cp.Position.Id == positionID);

        _context.CompanyPositions.Remove(companyPosition);
        
        _context.SaveChanges();
    }

    public void AddPositionToOpenPositionsById(int companyId,int positionID)
    {
        Position position = _context.Positions.SingleOrDefault(p => p.Id == positionID);
        CompanyPosition companyPosition = new CompanyPosition(position);
        Company company = _context.Companies.SingleOrDefault(c => c.Id == companyId);

        company.OpenPositions.Add(companyPosition);
        _context.SaveChanges();
    }

    public ICollection<Position> GetCompanyPositions(int id)
    {
        var positions = _context.Companies
            .Include(c => c.OpenPositions)
            .ThenInclude(op => op.Position)
            .SingleOrDefault(c => c.Id == id)
            .OpenPositions
            .Select(op => op.Position);
        
        return positions.ToList();
    }
}
