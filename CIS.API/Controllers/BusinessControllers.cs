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
        // Passes both models to the repository for a single transaction save
        await _repository.UpsertAsync(req.Business, req.Address);
        return Ok();
    }

    [HttpPost("interest")]
    public async Task<IActionResult> UpsertInterest(BusinessInterestDTO.PageModel req)
    {
        await _repository.UpsertAsync(req);
        return Ok();
    }
}