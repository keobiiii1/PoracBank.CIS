using CIS.API.Data;
using CIS.API.Repositories;
using CIS.Assets;
using CIS.Assets.DTO;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- 1. CORE SERVICES ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// --- 2. DATABASE ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddPooledDbContextFactory<CISDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<CISDbContext>(p =>
    p.GetRequiredService<IDbContextFactory<CISDbContext>>().CreateDbContext());

// --- 3. AUTOMAPPER ---
builder.Services.AddAutoMapper(typeof(CustomerDTO.MappingProfile).Assembly);

// --- 4. REPOSITORIES ---
builder.Services.AddScoped<ITransactionPolicy, DefaultTransactionPolicy>();
builder.Services.AddScoped<CustomerRepository>();
builder.Services.AddScoped<IndividualRepository>();
builder.Services.AddScoped<BankReviewRepository>();
builder.Services.AddScoped<BusinessRepository>();
builder.Services.AddScoped<ProfileRepository>();

// --- 5. SWAGGER ---
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.FullName!.Replace("+", "_"));
});

// --- 6. CORS ---
// !! Add your CIS.Client port to Cors:AllowedOrigins in appsettings.json !!
// e.g. "https://localhost:7186,http://localhost:5186"
// Check CIS.Client/Properties/launchSettings.json for the exact port.
var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>()
    ?? builder.Configuration["Cors:AllowedOrigins"]?.Split(',', StringSplitOptions.RemoveEmptyEntries)
    ?? Array.Empty<string>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorPolicy", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// --- 7. KYC IMAGE UPLOADS — raise Kestrel body limit to 10 MB ---
builder.WebHost.ConfigureKestrel(opts =>
    opts.Limits.MaxRequestBodySize = 10 * 1024 * 1024);

var app = builder.Build();

// --- 8. MIDDLEWARE (order matters) ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();  // added — redirects HTTP → HTTPS
app.UseStaticFiles();       // serves wwwroot/uploads/kyc/** as static URLs
app.UseRouting();           // added — required before UseCors/MapControllers
app.UseCors("BlazorPolicy");
app.UseAuthorization();
app.MapControllers();

app.Run();