using CIS.Assets.DTO;
using System.Net.Http.Json;

namespace CIS.Client.Services;

public class IndividualService
{
    private readonly HttpClient _http;
    private const string BaseUrl = "api/individual";

    public IndividualService(HttpClient http) => _http = http;

    public async Task<IndividualInfoDTO.PageModel?> UpsertInfoAsync(IndividualInfoDTO.PageModel request)
    {
        var response = await _http.PostAsJsonAsync($"{BaseUrl}/info", request);

        if (response.IsSuccessStatusCode)
        {
            if (response.Content.Headers.ContentLength > 0)
            {
                return await response.Content.ReadFromJsonAsync<IndividualInfoDTO.PageModel>();
            }
            return request;
        }

        return null;
    }

    public async Task UpsertEmploymentAsync(IndividualEmploymentDTO.PageModel request) =>
        await _http.PostAsJsonAsync($"{BaseUrl}/employment", request);

    public async Task UpsertFamilyAsync(IndividualFamilyDTO.PageModel request) =>
        await _http.PostAsJsonAsync($"{BaseUrl}/family", request);

    public async Task UpsertIdentificationAsync(IndividualIdentificationDTO.PageModel request) =>
        await _http.PostAsJsonAsync($"{BaseUrl}/identification", request);

    public async Task<long> FinalizeRegistrationAsync(IndividualRegistrationRequest request)
    {
        var response = await _http.PostAsJsonAsync($"{BaseUrl}/submit", request);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<long>();
        }

        return 0;
    }

    public async Task<IndividualRegistrationRequest?> GetRegistrationDetailsAsync(long customerId)
    {
        try
        {
            return await _http.GetFromJsonAsync<IndividualRegistrationRequest>($"{BaseUrl}/details/{customerId}");
        }
        catch (Exception)
        {
            return null;
        }
    }
}