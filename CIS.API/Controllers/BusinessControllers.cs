using CIS.API.Repositories;
using CIS.Assets.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CIS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BusinessController : ControllerBase
{
    private readonly BusinessRepository _repository;

    public BusinessController(BusinessRepository repo) => _repository = repo;

    [HttpPost("info")]
    public async Task<IActionResult> UpsertBusiness(BusinessInfoDTO.BusinessSaveRequest req)
    {
        await _repository.UpsertAsync(req.Business, req.Address);

        return Ok(req);
    }

    [HttpPost("interest")]
    public async Task<IActionResult> UpsertInterest(BusinessInterestDTO.PageModel req)
    {
        await _repository.UpsertAsync(req);
        return Ok();
    }

    [HttpPost("upsert/beneficiary")]
    public async Task<IActionResult> UpsertBeneficiary(BeneficiaryDTO.PageModel req)
    {
        await _repository.UpsertBeneficiaryAsync(req);
        return Ok();
    }
}