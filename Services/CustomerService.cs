using CIS.Assets.Common;
using CIS.Assets.DTO;
using System.Net.Http.Json;

namespace CIS.Client.Services;

public class CustomerService
{
    private readonly HttpClient _http;
    private const string BaseUrl = "api/customer";

    public CustomerService(HttpClient http) => _http = http;

    public async Task<CollectionDataSet<CustomerDTO.Browse>> BrowseAsync(CustomerDTO.Filter filter)
    {
        var response = await _http.PostAsJsonAsync($"{BaseUrl}/browse", filter);
        return await response.Content.ReadFromJsonAsync<CollectionDataSet<CustomerDTO.Browse>>()
               ?? new CollectionDataSet<CustomerDTO.Browse>();
    }

    public async Task UpsertAsync(CustomerDTO.PageModel request)
    {
        await _http.PostAsJsonAsync($"{BaseUrl}/upsert", request);
    }

    public async Task DeleteAsync(long id)
    {
        await _http.DeleteAsync($"{BaseUrl}/{id}");
    }
}