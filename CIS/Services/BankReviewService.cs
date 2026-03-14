using CIS.Assets.Common;
using CIS.Assets.DTO;
using System.Net.Http.Json;

namespace CIS.Client.Services;

public class BusinessService
{
    private readonly HttpClient _http;
    public BusinessService(HttpClient http) => _http = http;

    public async Task UpsertBusinessAsync(BusinessInfoDTO.PageModel req) =>
        await _http.PostAsJsonAsync("api/business/info", req);
}