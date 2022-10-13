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
        if (!IsPositionRequestDataValid(positionRequest))
        {
            return Conflict("Position name can't be empty or null.");
        }

        if (DoesPositionAlreadyExist(_positionService.GetAll(), positionRequest))
        {
            return Conflict("Position already exists.");
        }

        var position = positionRequest.ToPosition();
        try
        {
            _positionService.Create(position);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }

        var uri = $"{Request.Scheme}://{Request.Host}{Request.PathBase}{Request.Path}/{position.Id}";
        return Created(uri, position.ToPositionRequest());
    }

    [HttpGet, Route("position/{id:int}")]
    public IActionResult GetPosition(int id)
    {
        try
        {
            var position = _positionService.GetById(id);
            return position == null
                ? NotFound($"Position with id:\"{id}\" not found.")
                : Ok(position.ToPositionRequest());
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
