using Blazored.LocalStorage;
using CIS.Client;
using CIS.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// ApiBaseUrl in wwwroot/appsettings.json — falls back to same origin in production
// (Nginx proxies /api/ to Kestrel on the same host)
var apiBaseUrl = builder.Configuration["ApiBaseUrl"]
    ?? builder.HostEnvironment.BaseAddress;

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(apiBaseUrl)
});

builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(CIS.Assets.DTO.CustomerDTO).Assembly);

builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<IndividualService>();
builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<BusinessService>();
builder.Services.AddScoped<BankReviewService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<RegistrationStorageService>();

await builder.Build().RunAsync();