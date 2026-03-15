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

    /// <summary>
    /// Update existing customer information.
    /// Creation logic is now handled by FinalizeRegistrationAsync in specific services.
    /// </summary>
    public async Task<long> UpdateAsync(CustomerDTO.PageModel request)
    {
        var response = await _http.PostAsJsonAsync($"{BaseUrl}/update", request);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<long>();
        }
        return 0;
    }

    public async Task DeleteAsync(long id)
    {
        await _http.DeleteAsync($"{BaseUrl}/{id}");
    }
}