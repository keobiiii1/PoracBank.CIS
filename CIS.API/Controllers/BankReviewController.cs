using CIS.Assets.Common;
using CIS.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using CIS.Assets.DTO;

namespace CIS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BankReviewController : ControllerBase
{
    private readonly BankReviewRepository _repository;
    public BankReviewController(BankReviewRepository repo) => _repository = repo;

    [HttpPost("review")]
    public async Task<IActionResult> UpsertReview(BankReviewDTO.PageModel req)
    {
        await _repository.UpsertAsync(req); return Ok();
    }
}