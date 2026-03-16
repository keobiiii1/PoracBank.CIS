using CIS.API.Repositories;
using CIS.Assets.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CIS.API.Controllers;

[ApiController]
[Route("api/individual")]
public class IndividualController : ControllerBase
{
    private readonly IndividualRepository _repository;
    public IndividualController(IndividualRepository repo) => _repository = repo;

    [HttpPost("info")]
    public async Task<IActionResult> UpsertInfo([FromBody] IndividualInfoDTO.PageModel request)
    {
        await _repository.UpsertInfoAsync(request);
        return Ok(request);
    }

    [HttpPost("employment")]
    public async Task<IActionResult> UpsertEmployment(IndividualEmploymentDTO.PageModel req)
    {
        await _repository.UpsertEmploymentAsync(req);
        return Ok();
    }

    [HttpPost("family")]
    public async Task<IActionResult> UpsertFamily(IndividualFamilyDTO.PageModel req)
    {
        await _repository.UpsertFamilyAsync(req);
        return Ok();
    }

    [HttpPost("identification")]
    public async Task<IActionResult> UpsertIdentification(IndividualIdentificationDTO.PageModel req)
    {
        await _repository.UpsertIdentificationAsync(req);
        return Ok();
    }

    [HttpPost("submit")]
    public async Task<IActionResult> SubmitFullRegistration([FromBody] IndividualRegistrationRequest request)
    {
        var customerId = await _repository.IndividualFullRegistrationAsync(request);
        return Ok(customerId);
    }

    [HttpGet("details/{customerId}")]
    public async Task<IActionResult> GetFullDetails(long customerId)
    {
        var result = await _repository.GetFullRegistrationDetailsAsync(customerId);
        if (result == null) return NotFound("Customer details not found.");

        return Ok(result);
    }
}