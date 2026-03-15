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

    [HttpPost("acknowledgement")]
    public async Task<IActionResult> UpsertAcknowledgement(ClientAcknowlegdementDTO.PageModel req)
    {
        await _repository.UpsertAcknowlegdementAsync(req);
        return Ok();
    }

    [HttpPost("bank-review")]
    public async Task<IActionResult> UpsertBankReview(BankReviewDTO.PageModel req)
    {
        await _bankReviewRepository.UpsertAsync(req);
        return Ok();
    }
}