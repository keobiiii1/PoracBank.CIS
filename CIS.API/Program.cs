using CIS.API.Data;
using CIS.API.Repositories;
using CIS.Assets;
using CIS.Assets.DTO;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- 1. CORE SERVICES ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// --- 2. DATABASE (Must be registered before Repositories) ---
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddPooledDbContextFactory<CISDbContext>(options =>
    options.UseSqlServer(connectionString));

// Bridge for Scoped access
builder.Services.AddScoped<CISDbContext>(p =>
    p.GetRequiredService<IDbContextFactory<CISDbContext>>().CreateDbContext());

// --- 3. AUTOMAPPER ---
// Registering by Assembly is safer to prevent DI circular dependencies
builder.Services.AddAutoMapper(typeof(CustomerDTO.MappingProfile).Assembly);

// --- 4. REPOSITORIES ---
builder.Services.AddScoped<ITransactionPolicy, DefaultTransactionPolicy>();
builder.Services.AddScoped<CustomerRepository>();
builder.Services.AddScoped<IndividualRepository>();
builder.Services.AddScoped<BankReviewRepository>();
builder.Services.AddScoped<BusinessRepository>();
builder.Services.AddScoped<ProfileRepository>();

// --- 5. SWAGGER (Keep this at the end of registrations) ---
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.FullName.Replace("+", "_"));
});

// --- 6. CORS ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorPolicy", policy =>
    {
        policy.WithOrigins("https://localhost:7034", "http://localhost:5244")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// --- 7. MIDDLEWARE ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("BlazorPolicy");
app.UseAuthorization();
app.MapControllers();

app.Run();