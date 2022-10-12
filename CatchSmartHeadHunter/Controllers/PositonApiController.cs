using CatchSmartHeadHunter.Core.Models;
using CatchSmartHeadHunter.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CatchSmartHeadHunter.Controllers;

[ApiController]
[Route("api")]
public class PositionApiController : ControllerBase
{
    private readonly IEntityService<Position> _positionService;

    public PositionApiController(IEntityService<Position> positionService)
    {
        _positionService = positionService;
    }

    [HttpGet, Route("positions")]
    public IActionResult GetAllPositions()
    {
        var positions = _positionService.GetAll();

        return Ok(positions);
    }

    [HttpPost, Route("position")]
    public IActionResult PostPosition(Position position)
    {
        _positionService.Create(position);

        return Created("Position added.", position);
    }

    [HttpGet, Route("position/{id:int}")]
    public IActionResult GetPosition(int id)
    {
        Position position;
        try
        {
            position = _positionService.GetById(id);
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }

        return Ok(position);
    }

    [HttpDelete, Route("position/{id:int}")]
    public IActionResult DeletePosition(int id)
    {
        try
        {
            var position = _positionService.GetById(id);
            _positionService.Delete(position);
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }

        return Ok($"Position with id:{id} deleted.");
    }
}
