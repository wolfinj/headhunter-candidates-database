using CatchSmartHeadHunter.Core.Models;

namespace CatchSmartHeadHunter.Core.Services;

public interface ICompanyService : IEntityService<Company>
{
    Company GetCompleteCompanyById(int id);
    ICollection<Company> GetCompleteCompanies();
    void AddPositionToOpenPositions(Company company, Position position);
    void RemovePositionFromCompanyById(int companyId,int positionID);
    void AddPositionToOpenPositionsById(int companyId, int positionID);
}
