using CatchSmartHeadHunter.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CatchSmartHeadHunter.Helpers;

public static class HelperFunctions
{
    public static ICollection<Company> GetCompanies(HhDbContext context)
    {
        return context.Companies
            .Include(c => c.OpenPositions)
            .ThenInclude(p => p.Position)
            .ToList();
    }
    public static ICollection<Position> GetPositions(HhDbContext context)
    {
        return context.Positions
            .ToList();
    }
}
