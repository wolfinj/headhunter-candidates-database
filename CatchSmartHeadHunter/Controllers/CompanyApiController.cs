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

    [HttpPost, Route("companyRequest")]
    public IActionResult PostCompany(CompanyRequest companyRequest)
    {
        if (!IsCompanyRequestDataValid(companyRequest))
        {
            return Conflict("Company name and e-mail can't be empty or null.");
        }

        if (DoesCompanyAlreadyExist(_companyService.GetCompleteCompanies(),companyRequest))
        {
            return Conflict("Company already exists");
        }
        
        var newCompany = companyRequest.ToCompany();
        _companyService.Create(newCompany);

        var uri = $"{Request.Scheme}://{Request.Host}{Request.PathBase}{Request.Path}/{newCompany.Id}";
        return Created(uri, newCompany.ToCompanyRequest());
    }

    [HttpGet, Route("companyRequest/{id:int}")]
    public IActionResult GetCompany(int id)
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

    [HttpDelete, Route("companyRequest/{id:int}")]
    public IActionResult DeleteCompany(int id)
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

    [HttpPut, Route("companyRequest/{id:int}/add-positionRequest")]
    public IActionResult AddPositionToCompany(int id, PositionRequest positionRequest)
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

    [HttpPut, Route("companyRequest/{companyId:int}/add-positionRequest-id/{positionId:int}")]
    public IActionResult AddPositionToCompanyById(int companyId, int positionId)
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

    [HttpDelete, Route("companyRequest/{companyId:int}/remove-positionRequest/{positionId:int}")]
    public IActionResult RemovePositionFromCompany(int companyId, int positionId)
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

    [HttpGet, Route("companyRequest/{id:int}/positions")]
    public IActionResult GetCompanyPositions(int id)
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
