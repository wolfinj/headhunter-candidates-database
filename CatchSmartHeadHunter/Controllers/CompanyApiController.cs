using CatchSmartHeadHunter.Core.Models;
using CatchSmartHeadHunter.Core.Services;
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

        return Ok(companies);
    }

    [HttpPost, Route("company")]
    public IActionResult PostCompany(Company company)
    {
        _companyService.Create(company);

        return Created("Company added.", company);
    }

    [HttpGet, Route("company/{id:int}")]
    public IActionResult GetCompany(int id)
    {
        var company = _companyService.GetCompleteCompanyById(id);
        return Ok(company);
    }

    [HttpDelete, Route("company/{id:int}")]
    public IActionResult DeleteCompany(int id)
    {
        try
        {
            var company = _companyService.GetCompleteCompanyById(id);
            _companyService.Delete(company);
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }

        return Ok($"Company with id:{id} deleted.");
    }

    [HttpPut, Route("company/{id:int}/add-position")]
    public IActionResult AddPositionToCompany(int id, Position position)
    {
        Company company;
        
        try
        {
            company = _companyService.GetCompleteCompanyById(id);
            _companyService.AddPositionToOpenPositions(company,position);
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
        return Ok(company);
    }
    
    [HttpPut, Route("company/{companyId:int}/add-position-id/{positionId:int}")]
    public IActionResult AddPositionToCompanyById(int companyId, int positionId)
    {
        Company company;
        
        try
        {
            company = _companyService.GetCompleteCompanyById(companyId);
            _companyService.AddPositionToOpenPositionsById(companyId,positionId);
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
        return Ok(company);
    }

    [HttpDelete, Route("company/{companyId:int}/remove-position/{positionId:int}")]
    public IActionResult RemovePositionFromCompany(int companyId, int positionId)
    {
        var company = _companyService.GetCompleteCompanyById(companyId);
        // var companyPosition = _context.CompanyPositions.SingleOrDefault(cp => cp.Position.Id == positionId);
        try
        {
            _companyService.RemovePositionFromCompanyById(companyId,positionId);
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }
        
        return Ok(company);
    }
    
    [HttpGet, Route("company/{id:int}/positions")]
    public IActionResult GetCompanyPositions(int id)
    {
        var company = _companyService.GetCompanyPositions(id);
        return Ok(company);
    }
    
}
