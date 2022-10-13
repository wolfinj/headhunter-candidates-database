using CatchSmartHeadHunter.Core.Exceptions;
using CatchSmartHeadHunter.Core.Models;
using CatchSmartHeadHunter.Core.Services;
using CatchSmartHeadHunter.Helpers;
using CatchSmartHeadHunter.RequestModels;
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

        return Ok(positions.ToPositionRequestList());
    }

    [HttpPost, Route("position")]
    public IActionResult PostPosition(PositionRequest positionRequest)
    {
        // ToDo Validate request data

        var position = positionRequest.ToPosition();
        try
        {
            _positionService.Create(position);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }

        return Created("Position added.", position.ToPositionRequest());
    }

    [HttpGet, Route("position/{id:int}")]
    public IActionResult GetPosition(int id)
    {
        try
        {
            var position = _positionService.GetById(id);
            return position == null ? 
                NotFound($"Position with id:\"{id}\" not found.") : 
                Ok(position.ToPositionRequest());
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpDelete, Route("position/{id:int}")]
    public IActionResult DeletePosition(int id)
    {
        try
        {
            var position = _positionService.GetById(id) ?? throw new PositionNotFoundException(id);

            _positionService.Delete(position);
        }
        catch (PositionNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }

        return Ok($"Position with id:{id} deleted.");
    }
}
