using CIS.API.Repositories;
using CIS.Assets.DTO;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BusinessController : ControllerBase
{
    private readonly BusinessRepository _repository;
    private readonly BankReviewRepository _bankReviewRepository;

    public BusinessController(
        BusinessRepository repo,
        BankReviewRepository bankReviewRepo)
    {
        _repository = repo;
        _bankReviewRepository = bankReviewRepo;
    }

    [HttpPost("bank-review")]
    public async Task<IActionResult> UpsertBankReview(BankReviewDTO.PageModel req)
    {
        await _bankReviewRepository.UpsertAsync(req);
        return Ok();
    }

    [HttpPost("submit")]
    public async Task<IActionResult> SubmitFullRegistration([FromBody] BusinessRegistrationRequest request)
    {
        // Capture the generated ID from the repo
        var customerId = await _repository.BusinessFullRegistration(request);
        return Ok(customerId);
    }
}