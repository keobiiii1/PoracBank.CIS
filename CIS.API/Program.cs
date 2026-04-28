using CIS.API.Data;
using CIS.API.Repositories;
using CIS.Assets;
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

// --- 7. KESTREL — body limit only ---
builder.WebHost.ConfigureKestrel(opts =>
{
    opts.Limits.MaxRequestBodySize = 10 * 1024 * 1024;
});

var app = builder.Build();

// --- 8. MIDDLEWARE (order matters) ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseRouting();
app.UseCors("BlazorPolicy");
app.UseAuthorization();
app.MapControllers();

app.Run();
