using CatchSmartHeadHunter.Core.Exceptions;
using CatchSmartHeadHunter.Core.Models;
using CatchSmartHeadHunter.Core.Services;
using CatchSmartHeadHunter.Helpers;
using CatchSmartHeadHunter.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace CatchSmartHeadHunter.Controllers;

[ApiController]
[Route("api")]
public class CompanyApiController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompanyApiController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpGet, Route("companies")]
    public IActionResult GetAllCompanies()
    {
        var companies = _companyService.GetCompleteCompanies();

        return Ok(companies.ToCompanyRequestList());
    }

    [HttpPost, Route("company")]
    public IActionResult PostCompany(CompanyRequest company)
    {
        // Todo Validate request data
        var newCompany = company.ToCompany();
        _companyService.Create(newCompany);

        return Created("Company added.", newCompany.ToCompanyRequest());
    }

    [HttpGet, Route("company/{id:int}")]
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

    [HttpDelete, Route("company/{id:int}")]
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

    [HttpPut, Route("company/{id:int}/add-positionRequest")]
    public IActionResult AddPositionToCompany(int id, PositionRequest positionRequest)
    {
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

    [HttpPut, Route("company/{companyId:int}/add-positionRequest-id/{positionId:int}")]
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

    [HttpDelete, Route("company/{companyId:int}/remove-positionRequest/{positionId:int}")]
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

    [HttpGet, Route("company/{id:int}/positions")]
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
