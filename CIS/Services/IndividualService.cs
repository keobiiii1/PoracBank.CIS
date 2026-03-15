using CIS.Assets.DTO;
using System.Net.Http.Json;

namespace CIS.Client.Services;

public class IndividualService
{
    private readonly HttpClient _http;
    private const string BaseUrl = "api/individual";

    public IndividualService(HttpClient http) => _http = http;

    public async Task UpsertInfoAsync(IndividualInfoDTO.PageModel request) =>
        await _http.PostAsJsonAsync($"{BaseUrl}/info", request);

    public async Task UpsertEmploymentAsync(IndividualEmploymentDTO.PageModel request) =>
        await _http.PostAsJsonAsync($"{BaseUrl}/employment", request);

    public async Task UpsertFamilyAsync(IndividualFamilyDTO.PageModel request) =>
        await _http.PostAsJsonAsync($"{BaseUrl}/family", request);

    public async Task UpsertIdentificationAsync(IndividualIdentificationDTO.PageModel request) =>
        await _http.PostAsJsonAsync($"{BaseUrl}/identification", request);
}