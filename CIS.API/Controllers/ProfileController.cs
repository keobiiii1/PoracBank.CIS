using CIS.Assets.Common;
using CIS.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using CIS.Assets.DTO;

namespace CIS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfileController : ControllerBase
{
    private readonly ProfileRepository _repository;
    public ProfileController(ProfileRepository repo) => _repository = repo;

    [HttpPost("address")]
    public async Task<IActionResult> UpsertAddress(AddressDTO.PageModel req)
    {
        await _repository.UpsertAsync(req); return Ok();
    }

    [HttpPost("contact")]
    public async Task<IActionResult> UpsertContact(ContactDTO.PageModel req)
    {
        await _repository.UpsertAsync(req); return Ok();
    }
}