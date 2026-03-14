using CIS.Assets.Common;
using CIS.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using CIS.Assets.DTO;

namespace CIS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BusinessController : ControllerBase
{
    private readonly BusinessRepository _repository;
    public BusinessController(BusinessRepository repo) => _repository = repo;

    [HttpPost("info")]
    public async Task<IActionResult> UpsertBusiness(BusinessInfoDTO.PageModel req)
    {
        await _repository.UpsertAsync(req); return Ok();
    }

    [HttpPost("interest")]
    public async Task<IActionResult> UpsertInterest(BusinessInterestDTO.PageModel req)
    {
        await _repository.UpsertAsync(req); return Ok();
    }
}