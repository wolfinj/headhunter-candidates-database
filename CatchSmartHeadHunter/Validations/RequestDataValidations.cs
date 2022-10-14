using CatchSmartHeadHunter.Core.Models;
using CatchSmartHeadHunter.RequestModels;

namespace CatchSmartHeadHunter.Validations;

public static class RequestDataValidations
{
    // ======================================== Position ========================================
    #region Position

    public static bool IsPositionRequestDataValid(PositionRequest positionRequest)
    {
        return !string.IsNullOrEmpty(positionRequest.PositionName);
    }

    private static bool IsPositionSame(Position currentPosition, PositionRequest expectedPositionRequest)
    {
        return currentPosition.PositionName == expectedPositionRequest.PositionName;
    }

    public static bool DoesPositionAlreadyExist(ICollection<Position> positionList, PositionRequest currentPosition)
    {
        return positionList.Any(position => IsPositionSame(position, currentPosition));
    }

    #endregion

    // ======================================== Candidate ========================================
    #region Candidate

    public static bool IsCandidateRequestDataValid(CandidateRequest candidateRequest)
    {
        return !string.IsNullOrEmpty(candidateRequest.FullName) && !string.IsNullOrEmpty(candidateRequest.Email);
    }

    private static bool IsCandidateSame(Candidate currentCandidate, CandidateRequest expectedCandidateRequest)
    {
        return string.Equals(currentCandidate.FullName.Trim(),
                   expectedCandidateRequest.FullName.Trim(),
                   StringComparison.OrdinalIgnoreCase) &&
               string.Equals(currentCandidate.Email.Trim(),
                   expectedCandidateRequest.Email.Trim(),
                   StringComparison.OrdinalIgnoreCase);
    }

    public static bool DoesCandidateAlreadyExist(ICollection<Candidate> candidateList,
        CandidateRequest currentCandidate)
    {
        return candidateList.Any(candidate => IsCandidateSame(candidate, currentCandidate));
    }

    #endregion

    // ======================================== Company ========================================
    #region Company

    public static bool IsCompanyRequestDataValid(CompanyRequest companyRequest)
    {
        return !string.IsNullOrEmpty(companyRequest.Name) && !string.IsNullOrEmpty(companyRequest.Email);
    }

    private static bool IsCompanySame(Company currentCompany, CompanyRequest expectedCompanyRequest)
    {
        return string.Equals(currentCompany.Name.Trim(),
                   expectedCompanyRequest.Name.Trim(),
                   StringComparison.OrdinalIgnoreCase) &&
               string.Equals(currentCompany.Email.Trim(),
                   expectedCompanyRequest.Email.Trim(),
                   StringComparison.OrdinalIgnoreCase);
    }

    public static bool DoesCompanyAlreadyExist(ICollection<Company> companyList, CompanyRequest currentCompany)
    {
        return companyList.Any(company => IsCompanySame(company, currentCompany));

    }

    #endregion
}
