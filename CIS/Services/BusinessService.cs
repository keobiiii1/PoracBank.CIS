using CIS.Assets.DTO;
using CIS.Assets.Models;
using System.Net.Http.Json;

namespace CIS.Client.Services;

public class BusinessService
{
    private readonly HttpClient _http;
    private const string BaseUrl = "api/business";

    public BusinessService(HttpClient http) => _http = http;
    public async Task<BusinessInfoDTO.BusinessSaveRequest?> UpsertBusinessInfoAsync(BusinessInfoDTO.PageModel business, AddressDTO.PageModel address)
    {
        var request = new BusinessInfoDTO.BusinessSaveRequest { Business = business, Address = address };
        var response = await _http.PostAsJsonAsync($"{BaseUrl}/info", request);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<BusinessInfoDTO.BusinessSaveRequest>();
        }
        return null;
    }

    public async Task UpsertBeneficiaryAsync(BeneficiaryDTO.PageModel request)
    {
        await _http.PostAsJsonAsync($"{BaseUrl}/upsert/beneficiary", request);
    }

    public async Task UpsertInterestAsync(BusinessInterestDTO.PageModel request) =>
        await _http.PostAsJsonAsync($"{BaseUrl}/interest", request);

    public async Task UpsertAcknowledgementAsync(ClientAcknowlegdementDTO.PageModel request)
    {
        await _http.PostAsJsonAsync($"{BaseUrl}/acknowledgement", request);
    }

    public async Task UpsertBankReviewAsync(BankReviewDTO.PageModel request)
    {
        await _http.PostAsJsonAsync($"{BaseUrl}/bank-review", request);
    }
}