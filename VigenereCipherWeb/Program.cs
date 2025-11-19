using Auth0.AspNetCore.Authentication;
using VigenereCipherWeb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var dbType = builder.Configuration["DatabaseType"];


builder.Services.AddControllersWithViews();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "Vigenere API v1", 
        Version = "v1" 
    });

    options.SwaggerDoc("v2", new OpenApiInfo 
    { 
        Title = "Vigenere API v2", 
        Version = "v2" 
    });

    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    switch (dbType)
    {
        case "SqlServer":
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            break;
        case "Postgres":
            options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"));
            break;
        case "Sqlite":
            options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"));
            break;
        case "InMemory":
            options.UseInMemoryDatabase("VigenereCipherDb");
            break;
        default:
            throw new Exception("Database type not configured");
    }
});

builder.Services
    .AddAuth0WebAppAuthentication(options =>
    {
        options.Domain = builder.Configuration["Auth0:Domain"] 
            ?? throw new InvalidOperationException("Auth0:Domain is not configured");
        options.ClientId = builder.Configuration["Auth0:ClientId"] 
            ?? throw new InvalidOperationException("Auth0:ClientId is not configured");
        options.ClientSecret = builder.Configuration["Auth0:ClientSecret"] 
            ?? throw new InvalidOperationException("Auth0:ClientSecret is not configured");
        options.CallbackPath = new PathString("/callback");
        options.Scope = "openid profile email";
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var currentDbType = app.Configuration["DatabaseType"];

    if (currentDbType == "SqlServer" ||
        currentDbType == "Postgres" ||
        currentDbType == "Sqlite")
    {
        db.Database.Migrate();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Vigenere API v1");
    options.SwaggerEndpoint("/swagger/v2/swagger.json", "Vigenere API v2");
});

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
public partial class Program { }