using CIS.Assets.Common;
using CIS.Assets.DTO;
using System.Net.Http.Json;

namespace CIS.Client.Services;

public class BankReviewService
{
    private readonly HttpClient _http;
    public BankReviewService(HttpClient http) => _http = http;

    public async Task UpsertReviewAsync(BankReviewDTO.PageModel req) =>
        await _http.PostAsJsonAsync("api/bankreview/review", req);
}