using CatchSmartHeadHunter.Core.Models;
using CatchSmartHeadHunter.Core.Services;
using CatchSmartHeadHunter.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CatchSmartHeadHunter.Controllers;

[ApiController]
[Route("api")]
public class CandidateApiController : ControllerBase
{
    private readonly ICandidateService _candidateService;

    public CandidateApiController(ICandidateService candidateService)
    {
        _candidateService = candidateService;
    }

    [HttpGet, Route("candidates")]
    public IActionResult GetAllCandidates()
    {
        var candidates = _candidateService.GetCompleteCandidates();

        return Ok(candidates);
    }

    [HttpPost, Route("candidate")]
    public IActionResult PostCandidate(Candidate candidate)
    {
        _candidateService.Create(candidate);

        return Created("Candidate added.", candidate);
    }

    [HttpGet, Route("candidate/{id:int}")]
    public IActionResult GetCandidate(int id)
    {
        var candidate = _candidateService.GetCompleteCandidateById(id);

        return Ok(candidate);
    }

    [HttpDelete, Route("candidate/{id:int}")]
    public IActionResult DeleteCandidate(int id)
    {
        try
        {
            var candidate = _candidateService.GetCompleteCandidateById(id);

            _candidateService.Delete(candidate);
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }

        return Ok($"Candidate with id:{id} deleted.");
    }

    [HttpPut, Route("candidate/{candidateId:int}/add-position-id/{positionId:int}")]
    public IActionResult AddPositionToCandidateById(int candidateId, int positionId)
    {
        Candidate candidate;

        try
        {
            candidate = _candidateService.GetCompleteCandidateById(candidateId);

            _candidateService.AddPositionToAppliedPositionsById(candidateId, positionId);
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }

        return Ok(candidate);
    }

    [HttpDelete, Route("candidate/{candidateId:int}/remove-position/{positionId:int}")]
    public IActionResult RemovePositionFromCandidate([FromRoute] int candidateId, [FromRoute] int positionId)
    {
        var candidate = _candidateService.GetCompleteCandidateById(candidateId);

        try
        {
            _candidateService.RemovePositionFromCandidateById(candidateId, positionId);
        }
        catch (Exception e)
        {
            return Conflict(e.Message);
        }

        return Ok(candidate);
    }

    [HttpGet, Route("candidate/{candidateId:int}/companies-applied-to")]
    public IActionResult CompaniesCandidateAppliedTo(int candidateId)
    {
        var companies = _candidateService.GetCompanies(candidateId);
        return Ok(companies);
    }
}
