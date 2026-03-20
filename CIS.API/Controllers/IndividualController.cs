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