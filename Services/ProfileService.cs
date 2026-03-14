using CIS.Assets.Common;
using CIS.Assets.DTO;
using System.Net.Http.Json;

namespace CIS.Client.Services;

public class ProfileService
{
    private readonly HttpClient _http;
    private const string BaseUrl = "api/profile";

    public ProfileService(HttpClient http) => _http = http;

    public async Task UpsertAddressAsync(AddressDTO.PageModel request) =>
        await _http.PostAsJsonAsync($"{BaseUrl}/address", request);

    public async Task UpsertContactAsync(ContactDTO.PageModel request) =>
        await _http.PostAsJsonAsync($"{BaseUrl}/contact", request);
}