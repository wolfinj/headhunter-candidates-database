using CatchSmartHeadHunter.Core.Models;
using CatchSmartHeadHunter.RequestModels;

namespace CatchSmartHeadHunter.Validations;

public static class RequestDataValidations
{
    // ======================================== Position ========================================

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
        foreach (var position in positionList)
        {
            var isPositionSame = IsPositionSame(position, currentPosition);

            if (isPositionSame)
            {
                return true;
            }
        }

        return false;
    }

    // ======================================== Candidate ========================================

    public static bool IsCandidateRequestDataValid(CandidateRequest candidateRequest)
    {
        return !string.IsNullOrEmpty(candidateRequest.FullName) && !string.IsNullOrEmpty(candidateRequest.Email);
    }

    private static bool IsCandidateSame(Candidate currentCandidate, CandidateRequest expectedCandidateRequest)
    {
        return string.Equals(currentCandidate.FullName.Trim(), expectedCandidateRequest.FullName.Trim(),
                   StringComparison.OrdinalIgnoreCase) &&
               string.Equals(currentCandidate.Email.Trim(), expectedCandidateRequest.Email.Trim(),
                   StringComparison.OrdinalIgnoreCase);
    }

    public static bool DoesCandidateAlreadyExist(ICollection<Candidate> candidateList,
        CandidateRequest currentCandidate)
    {
        foreach (var candidate in candidateList)
        {
            var isCandidateSame = IsCandidateSame(candidate, currentCandidate);

            if (isCandidateSame)
            {
                return true;
            }
        }

        return false;
    }

    // ======================================== Company ========================================

    public static bool IsCompanyRequestDataValid(CompanyRequest companyRequest)
    {
        return !string.IsNullOrEmpty(companyRequest.Name) && !string.IsNullOrEmpty(companyRequest.Email);
    }

    private static bool IsCompanySame(Company currentCompany, CompanyRequest expectedCompanyRequest)
    {
        return string.Equals(currentCompany.Name.Trim(), expectedCompanyRequest.Name.Trim(),
                   StringComparison.OrdinalIgnoreCase) &&
               string.Equals(currentCompany.Email.Trim(), expectedCompanyRequest.Email.Trim(),
                   StringComparison.OrdinalIgnoreCase);
    }

    public static bool DoesCompanyAlreadyExist(ICollection<Company> companyList, CompanyRequest currentCompany)
    {
        foreach (var company in companyList)
        {
            var isCompanySame = IsCompanySame(company, currentCompany);

            if (isCompanySame)
            {
                return true;
            }
        }

        return false;
    }
}
