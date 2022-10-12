using CatchSmartHeadHunter.Core.Models;
using CatchSmartHeadHunter.Data;
using CatchSmartHeadHunter.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static CatchSmartHeadHunter.Helpers.HelperFunctions;

namespace CatchSmartHeadHunter.Controllers;

[ApiController]
[Route("api")]
public class ClientApiController : ControllerBase
{
    private readonly HhDbContext _context;

    public ClientApiController(HhDbContext context)
    {
        _context = context;
    }

    [HttpGet, Route("companies")]
    public IActionResult GetAllCompanies()
    {
        var companies = GetCompanies(_context);

        return Ok(companies);
    }

    [HttpPost, Route("company")]
    public IActionResult PostCompany(Company company)
    {
        _context.Add(company);
        _context.SaveChanges();

        return Created("Company added.", company);
    }

    [HttpGet, Route("company/{id:int}")]
    public IActionResult GetCompany(int id)
    {
        return Ok(_context.Companies
            .Include(c => c.OpenPositions)!
            .ThenInclude(op => op.Position)
            .SingleOrDefault(c => c.Id == id));
    }

    [HttpDelete, Route("company/{id:int}")]
    public IActionResult DeleteCompany(int id)
    {
        var company = _context.Companies.SingleOrDefault(c => c.Id == id);
        _context.CompanyPositions.RemoveRange(company.OpenPositions);
        _context.Companies.Remove(company);
        _context.SaveChanges();

        return Ok($"Company with id:{id} deleted.");
    }

    [HttpPut, Route("company/{id:int}/add-position")]
    public IActionResult AddPositionToCompany(int id, Position position)
    {
        var company = _context.Companies.SingleOrDefault(c => c.Id == id);
        var companyPosition = new CompanyPosition(position);
        _context.Add(position);
        company.OpenPositions.Add(companyPosition);
        _context.SaveChanges();

        return Ok(company);
    }

    [HttpDelete, Route("company/{companyId:int}/remove-position/{posId:int}")]
    public IActionResult RemovePositionFromCompany(int companyId, int posId)
    {
        var companyPosition = _context.CompanyPositions.SingleOrDefault(cp => cp.Position.Id == posId);
        var company = _context.Companies.SingleOrDefault(c => c.Id == companyId);

        company.OpenPositions.Remove(companyPosition);
        _context.SaveChanges();

        return Ok(company);
    }
}
