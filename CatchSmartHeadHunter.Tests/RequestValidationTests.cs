using CatchSmartHeadHunter.Core.Models;
using CatchSmartHeadHunter.RequestModels;
using static CatchSmartHeadHunter.Validations.RequestDataValidations;

namespace CatchSmartHeadHunter.Tests;

public class RequestValidationTests
{
    private PositionRequest _positionRequest;
    private Position _position;
    private ICollection<Position> _positions;

    private CandidateRequest _candidateRequest;
    private Candidate _candidate;
    private ICollection<Candidate> _candidates;

    private CompanyRequest _companyRequest;
    private Company _company;
    private ICollection<Company> _companies;

    public RequestValidationTests()
    {
        _positionRequest = new PositionRequest
        {
            PositionName = "Tester",
            Description = "Write unit tests for products."
        };

        _position = new Position
        {
            Id = 1,
            PositionName = "Tester",
            Description = "Write unit tests for products."
        };

        _positions = new List<Position>();

        _candidateRequest = new CandidateRequest
        {
            FullName = "John Smoll",
            Email = "john@home.co",
            About = "Enthusiastic to work.",
            Skills = new[]
            {
                "C#",
                "xUnit"
            }
        };

        _candidate = new Candidate
        {
            Id = 1,
            FullName = "John Smoll",
            Email = "john@home.co",
            About = "Enthusiastic to work.",
            Skills = new List<Skill>
            {
                new("C#"),
                new("xUnit")
            }
        };

        _candidates = new List<Candidate>();

        _companyRequest = new CompanyRequest
        {
            Name = "Future inc.",
            Email = "pr@future.com",
            Description = "Working in future"
        };

        _company = new Company
        {
            Id = 1,
            Name = "Future inc.",
            Email = "pr@future.com",
            Description = "Working in future"
        };

        _companies = new List<Company>();
    }

    [Fact]
    public void IsPositionRequestDataValid_PassValidData_ExpectToBeTrue()
    {
        // Act 
        var act = IsPositionRequestDataValid(_positionRequest);

        // Assert
        act.Should().BeTrue();
    }

    [Fact]
    public void IsPositionRequestDataValid_PassDataWithEmptyName_ExpectToBeFalse()
    {
        // Arrange
        _positionRequest.PositionName = "";

        // Act
        var act = IsPositionRequestDataValid(_positionRequest);

        // Assert
        act.Should().BeFalse();
    }

    [Fact]
    public void DoesPositionAlreadyExist_AddSameDataPositionToCollection_ExpectedToBeTrue()
    {
        // Arrange
        _positions.Add(_position);

        // Act
        var act = DoesPositionAlreadyExist(_positions, _positionRequest);

        // Assert
        act.Should().BeTrue();
    }

    [Fact]
    public void DoesPositionAlreadyExist_AddDifferentDataPositionToCollection_ExpectedToBeFalse()
    {
        // Arrange
        _position.PositionName = "Guard";
        _positions.Add(_position);

        // Act
        var act = DoesPositionAlreadyExist(_positions, _positionRequest);

        // Assert
        act.Should().BeFalse();
    }

    [Fact]
    public void IsCandidateRequestDataValid_PassValidData_ExpectToBeTrue()
    {
        // Act
        var act = IsCandidateRequestDataValid(_candidateRequest);

        // Assert
        act.Should().BeTrue();
    }

    [Fact]
    public void IsCandidateRequestDataValid_PassDataWithEmptyName_ExpectToBeFalse()
    {
        // Arrange
        _candidateRequest.FullName = "";

        // Act
        var act = IsCandidateRequestDataValid(_candidateRequest);

        // Assert
        act.Should().BeFalse();
    }

    [Fact]
    public void DoesCandidateAlreadyExist_AddSameDataPositionToCollection_ExpectedToBeTrue()
    {
        // Arrange
        _candidates.Add(_candidate);

        // Act
        var act = DoesCandidateAlreadyExist(_candidates, _candidateRequest);

        // Assert
        act.Should().BeTrue();
    }

    [Fact]
    public void DoesCandidateAlreadyExist_AddDifferentDataPositionToCollection_ExpectedToBeFalse()
    {
        // Arrange
        _candidate.FullName = "Henry Smith";
        _candidates.Add(_candidate);

        // Act
        var act = DoesCandidateAlreadyExist(_candidates, _candidateRequest);

        // Assert
        act.Should().BeFalse();
    }

    [Fact]
    public void IsCompanyRequestDataValid_PassValidData_ExpectToBeTrue()
    {
        // Act
        var act = IsCompanyRequestDataValid(_companyRequest);

        // Assert
        act.Should().BeTrue();
    }

    [Fact]
    public void IsCompanyRequestDataValid_PassDataWithEmptyName_ExpectToBeFalse()
    {
        // Arrange
        _companyRequest.Name = "";

        // Act
        var act = IsCompanyRequestDataValid(_companyRequest);

        // Assert
        act.Should().BeFalse();
    }

    [Fact]
    public void DoesCompanyAlreadyExist_AddSameDataPositionToCollection_ExpectedToBeTrue()
    {
        // Arrange
        _companies.Add(_company);

        // Act
        var act = DoesCompanyAlreadyExist(_companies, _companyRequest);

        // Assert
        act.Should().BeTrue();
    }

    [Fact]
    public void DoesCompanyAlreadyExist_AddDifferentDataPositionToCollection_ExpectedToBeFalse()
    {
        // Arrange
        _company.Name = "Past corp.";
        _companies.Add(_company);

        // Act
        var act = DoesCompanyAlreadyExist(_companies, _companyRequest);

        // Assert
        act.Should().BeFalse();
    }
}
