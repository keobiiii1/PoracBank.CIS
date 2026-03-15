using CIS.Assets.DTO;
using System.Net.Http.Json;

namespace CIS.Client.Services;

public class BusinessService
{
    private readonly HttpClient _http;
    private const string BaseUrl = "api/business";

    public BusinessService(HttpClient http) => _http = http;

    public async Task UpsertBusinessInfoAsync(BusinessInfoDTO.BusinessSaveRequest request) =>
        await _http.PostAsJsonAsync($"{BaseUrl}/info", request);

    public async Task UpsertInterestAsync(BusinessInterestDTO.PageModel request) =>
        await _http.PostAsJsonAsync($"{BaseUrl}/interest", request);
}