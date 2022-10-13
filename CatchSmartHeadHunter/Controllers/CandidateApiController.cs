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

        return Ok(candidates.ToCandidateRequestList());
    }

    [HttpPost, Route("candidate")]
    public IActionResult PostCandidate([FromBody] CandidateRequest candidateRequest)
    {
        if (!IsCandidateRequestDataValid(candidateRequest))
        {
            return Conflict("Candidate name and e-mail can't be empty or null.");
        }

        if (DoesCandidateAlreadyExist(_candidateService.GetCompleteCandidates(), candidateRequest))
        {
            return Conflict("Candidate already exists");
        }

        var candidate = candidateRequest.ToCandidate();

        _candidateService.Create(candidate);

        var uri = $"{Request.Scheme}://{Request.Host}{Request.PathBase}{Request.Path}/{candidate.Id}";

        return Created(uri, candidate.ToCandidateRequest());
    }

    [HttpGet, Route("candidate/{id:int}")]
    public IActionResult GetCandidate([FromRoute] int id)
    {
        Candidate candidate;

        try
        {
            candidate = _candidateService.GetCompleteCandidateById(id);
        }
        catch (CandidateNotFoundException e)
        {
            return NotFound(e.Message);
        }

        return Ok(candidate.ToCandidateRequest());
    }

    [HttpDelete, Route("candidate/{id:int}")]
    public IActionResult DeleteCandidate([FromRoute] int id)
    {
        try
        {
            var candidate = _candidateService.GetCompleteCandidateById(id);

            _candidateService.Delete(candidate);
        }
        catch (CandidateNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }

        return Ok($"Candidate with id:\"{id}\" deleted.");
    }

    [HttpPut, Route("candidate/{candidateId:int}/add-position-id/{positionId:int}")]
    public IActionResult AddPositionToCandidateById([FromRoute] int candidateId, [FromRoute] int positionId)
    {
        Candidate candidate;

        try
        {
            candidate = _candidateService.GetCompleteCandidateById(candidateId);

            _candidateService.AddPositionToAppliedPositionsById(candidateId, positionId);
        }
        catch (CandidateNotFoundException e)
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

        return Ok(candidate.ToCandidateRequest());
    }

    [HttpDelete, Route("candidate/{candidateId:int}/remove-position/{positionId:int}")]
    public IActionResult RemovePositionFromCandidate([FromRoute] int candidateId, [FromRoute] int positionId)
    {
        Candidate candidate;
        try
        {
            candidate = _candidateService.GetCompleteCandidateById(candidateId);

            _candidateService.RemovePositionFromCandidateById(candidateId, positionId);
        }
        catch (CandidateNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ApliedPositionNotAvaibleException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }

        return Ok(candidate.ToCandidateRequest());
    }

    [HttpGet, Route("candidate/{candidateId:int}/companies-applied-to")]
    public IActionResult CompaniesCandidateAppliedTo([FromRoute] int candidateId)
    {
        ICollection<Company> companies;

        try
        {
            companies = _candidateService.GetCompanies(candidateId);
        }
        catch (CandidateNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }

        return Ok(companies.ToCompanyRequestList());
    }
}
