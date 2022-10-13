using CatchSmartHeadHunter.Core.Exceptions;
using CatchSmartHeadHunter.Core.Models;
using CatchSmartHeadHunter.Core.Services;
using CatchSmartHeadHunter.Helpers;
using CatchSmartHeadHunter.RequestModels;
using Microsoft.AspNetCore.Mvc;
using static CatchSmartHeadHunter.Validations.RequestDataValidations;

namespace CatchSmartHeadHunter.Controllers;

[ApiController]
[Route("api")]
public class CompanyApiController : ControllerBase
{
    private readonly ICompanyService _companyService;
    private readonly IEntityService<Position> _positionService;

    public CompanyApiController(ICompanyService companyService, IEntityService<Position> positionService)
    {
        _companyService = companyService;
        _positionService = positionService;
    }

    [HttpGet, Route("companies")]
    public IActionResult GetAllCompanies()
    {
        var companies = _companyService.GetCompleteCompanies();

        return Ok(companies.ToCompanyRequestList());
    }

    [HttpPost, Route("company")]
    public IActionResult PostCompany([FromBody] CompanyRequest companyRequest)
    {
        if (!IsCompanyRequestDataValid(companyRequest))
        {
            return Conflict("Company name and e-mail can't be empty or null.");
        }

        if (DoesCompanyAlreadyExist(_companyService.GetCompleteCompanies(), companyRequest))
        {
            return Conflict("Company already exists");
        }

        var newCompany = companyRequest.ToCompany();
        _companyService.Create(newCompany);

        var uri = $"{Request.Scheme}://{Request.Host}{Request.PathBase}{Request.Path}/{newCompany.Id}";
        return Created(uri, newCompany.ToCompanyRequest());
    }

    [HttpGet, Route("company/{id:int}")]
    public IActionResult GetCompany([FromRoute] int id)
    {
        Company company;

        try
        {
            company = _companyService.GetCompleteCompanyById(id);
        }
        catch (CompanyNotFoundException e)
        {
            return NotFound(e.Message);
        }

        return Ok(company.ToCompanyRequest());
    }

    [HttpDelete, Route("company/{id:int}")]
    public IActionResult DeleteCompany([FromRoute] int id)
    {
        try
        {
            var company = _companyService.GetCompleteCompanyById(id);
            _companyService.Delete(company);
        }
        catch (CompanyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }

        return Ok($"Company with id:{id} deleted.");
    }

    [HttpPut, Route("company/{id:int}/add-position")]
    public IActionResult AddPositionToCompany([FromRoute] int id, [FromBody] PositionRequest positionRequest)
    {
        if (!IsPositionRequestDataValid(positionRequest))
        {
            return Conflict("Position name can't be empty or null.");
        }

        if (DoesPositionAlreadyExist(_positionService.GetAll(), positionRequest))
        {
            return Conflict("Position already exists.");
        }

        Company company;

        try
        {
            company = _companyService.GetCompleteCompanyById(id);
            _companyService.AddPositionToOpenPositions(company, positionRequest.ToPosition());
        }
        catch (CompanyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }

        return Ok(company);
    }

    [HttpPut, Route("company/{companyId:int}/add-position-id/{positionId:int}")]
    public IActionResult AddPositionToCompanyById([FromRoute] int companyId, [FromRoute] int positionId)
    {
        Company company;

        try
        {
            company = _companyService.GetCompleteCompanyById(companyId);
            _companyService.AddPositionToOpenPositionsById(companyId, positionId);
        }
        catch (CompanyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (PositionNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }

        return Ok(company);
    }

    [HttpDelete, Route("company/{companyId:int}/remove-position/{positionId:int}")]
    public IActionResult RemovePositionFromCompany([FromRoute] int companyId, [FromRoute] int positionId)
    {
        Company company;
        try
        {
            company = _companyService.GetCompleteCompanyById(companyId);
            _companyService.RemovePositionFromCompanyById(companyId, positionId);
        }
        catch (CompanyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (OpenPositionNotAvaibleException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }

        return Ok(company);
    }

    [HttpGet, Route("company/{id:int}/positions")]
    public IActionResult GetCompanyPositions([FromRoute] int id)
    {
        ICollection<Position> companyOpenPositions;

        try
        {
            companyOpenPositions = _companyService.GetCompanyPositions(id);
        }
        catch (CompanyNotFoundException e)
        {
            return NotFound(e.Message);
        }

        return Ok(companyOpenPositions);
    }
}
