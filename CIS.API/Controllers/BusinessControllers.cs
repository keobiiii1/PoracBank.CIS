using CIS.Assets.Common;
using CIS.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using CIS.Assets.DTO;
using static CIS.Assets.DTO.BusinessInfoDTO.PageModel;
using static CIS.Assets.DTO.BusinessInfoDTO;

namespace CIS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BusinessController : ControllerBase
{
    private readonly BusinessRepository _repository;
    public BusinessController(BusinessRepository repo) => _repository = repo;

    [HttpPost("info")]
    public async Task<IActionResult> UpsertBusiness([FromBody] BusinessSaveRequest req)
    {
        await _repository.UpsertAsync(req.Business, req.Address);
        return Ok();
    }

    [HttpPost("interest")]
    public async Task<IActionResult> UpsertInterest(BusinessInterestDTO.PageModel req)
    {
        await _repository.UpsertAsync(req); return Ok();
    }
}