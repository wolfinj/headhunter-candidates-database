using CatchSmartHeadHunter.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static CatchSmartHeadHunter.Helpers.HelperFunctions;

namespace CatchSmartHeadHunter.Controllers;

[ApiController]
[Route("api")]
public class PositionApiController : ControllerBase
{
    private readonly HhDbContext _context;

    public PositionApiController(HhDbContext context)
    {
        _context = context;
    }

    [HttpGet, Route("positions")]
    public IActionResult GetAllPositions()
    {
        var positions = GetPositions(_context);

        return Ok(positions);
    }

    [HttpPost, Route("position")]
    public IActionResult PostPosition(Position position)
    {
        _context.Add(position);
        _context.SaveChanges();

        return Created("Position added.", position);
    }

    [HttpGet, Route("position/{id:int}")]
    public IActionResult GetPosition(int id)
    {
        return Ok(_context.Positions
            .Include(p => p.RequiredSkills)!
            .SingleOrDefault(p => p.Id == id));
    }

    [HttpDelete, Route("position/{id:int}")]
    public IActionResult DeletePosition(int id)
    {
        var position = _context.Positions.SingleOrDefault(c => c.Id == id);
        var companyPositions = _context.CompanyPositions.Where(cp => cp.Position.Id == id);
        foreach (var cp in companyPositions)
        {
            var company = _context.Companies.SingleOrDefault(c => c.OpenPositions.Contains(cp));
            company.OpenPositions.Remove(cp);
        }
        _context.CompanyPositions.RemoveRange(companyPositions);
        _context.SaveChanges();

        _context.ReqSkills.RemoveRange(position.RequiredSkills);
        _context.Positions.Remove(position);
        
        
        _context.SaveChanges();

        return Ok($"Position with id:{id} deleted.");
    }
}