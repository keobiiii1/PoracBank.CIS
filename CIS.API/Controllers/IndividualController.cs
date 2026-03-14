using CIS.API.Repositories;
using CIS.Assets.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CIS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IndividualController : ControllerBase
{
    private readonly IndividualRepository _repository;
    public IndividualController(IndividualRepository repo) => _repository = repo;

    [HttpPost("info")]
    public async Task<IActionResult> UpsertInfo(IndividualInfoDTO.PageModel req)
    {
        // Fix: Call UpsertInfoAsync specifically
        await _repository.UpsertInfoAsync(req);
        return Ok();
    }

    [HttpPost("employment")]
    public async Task<IActionResult> UpsertEmployment(IndividualEmploymentDTO.PageModel req)
    {
        // Fix: Call UpsertEmploymentAsync specifically
        await _repository.UpsertEmploymentAsync(req);
        return Ok();
    }

    [HttpPost("family")]
    public async Task<IActionResult> UpsertFamily(IndividualFamilyDTO.PageModel req)
    {
        // Fix: Call UpsertFamilyAsync specifically
        await _repository.UpsertFamilyAsync(req);
        return Ok();
    }
}