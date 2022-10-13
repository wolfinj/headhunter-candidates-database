using CatchSmartHeadHunter.Core.Exceptions;
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
        var company = Context.Companies
            .Include(c => c.OpenPositions)
            .ThenInclude(op => op.Position)
            .SingleOrDefault(c => c.Id == id);

        if (company == null)
        {
            throw new CompanyNotFoundException(id);
        }

        return company;
    }

    public ICollection<Company> GetCompleteCompanies()
    {
        return Context.Companies
            .Include(c => c.OpenPositions)
            .ThenInclude(op => op.Position)
            .ToList();
    }

    public void AddPositionToOpenPositions(Company company, Position position)
    {
        var companyPosition = new CompanyPosition(position);
        Context.Add(position);
        company.OpenPositions.Add(companyPosition);
        Context.SaveChanges();
    }

    public void RemovePositionFromCompanyById(int companyId, int positionId)
    {
        var company = GetCompleteCompanyById(companyId);

        var companyPosition = company.OpenPositions.SingleOrDefault(cp => cp.Position.Id == positionId);

        if (companyPosition == null)
        {
            throw new OpenPositionNotAvaibleException(positionId);
        }

        Context.CompanyPositions.Remove(companyPosition);

        Context.SaveChanges();
    }

    public void AddPositionToOpenPositionsById(int companyId, int positionId)
    {
        var company = GetCompleteCompanyById(companyId);

        var position = Context.Positions.SingleOrDefault(p => p.Id == positionId);

        if (position == null)
        {
            throw new PositionNotFoundException(positionId);
        }

        var companyPosition = new CompanyPosition(position);

        company.OpenPositions.Add(companyPosition);
        Context.SaveChanges();
    }

    public ICollection<Position> GetCompanyPositions(int id)
    {
        var positions = GetCompleteCompanyById(id)
            .OpenPositions!
            .Select(op => op.Position);

        return positions.ToList();
    }
}
