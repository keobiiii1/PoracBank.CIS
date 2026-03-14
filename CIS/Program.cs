using CIS.Client;
using CIS.Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<IndividualService>();
builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<BusinessService>();
builder.Services.AddScoped<BankReviewService>();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7140")
});

await builder.Build().RunAsync();