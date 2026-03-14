using CIS.Assets.Common;
using CIS.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using CIS.Assets.DTO;

namespace CIS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly CustomerRepository _repository;

    public CustomerController(CustomerRepository repository) => _repository = repository;

    [HttpPost("browse")]
    public async Task<ActionResult<CollectionDataSet<CustomerDTO.Browse>>> Browse(CustomerDTO.Filter filter)
    {
        return Ok(await _repository.BrowseAsync(filter));
    }

    [HttpPost("upsert")]
    public async Task<IActionResult> Upsert(CustomerDTO.PageModel request)
    {

        var customerId = await _repository.UpsertAsync(request);
        return Ok(customerId);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        await _repository.DeleteAsync(id);
        return Ok();
    }
}
