using CIS.Assets.DTO;
using System.Net.Http.Json;

namespace CIS.Client.Services;

public class BusinessService
{
    private readonly HttpClient _httpClient;
    private const string BaseUrl = "api/business";

    public BusinessService(HttpClient httpClient) => _httpClient = httpClient;

    public async Task UpsertBusinessInfoAsync(BusinessInfoDTO.PageModel business, AddressDTO.PageModel address)
    {
        // Use the wrapper class defined in the DTO
        var request = new BusinessInfoDTO.BusinessSaveRequest
        {
            Business = business,
            Address = address
        };

        await _httpClient.PostAsJsonAsync($"{BaseUrl}/info", request);
    }
}