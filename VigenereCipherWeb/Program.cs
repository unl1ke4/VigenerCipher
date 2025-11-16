using Auth0.AspNetCore.Authentication;
using VigenereCipherWeb.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var dbType = builder.Configuration["DatabaseType"];

builder.Services.AddControllersWithViews();

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

// Автоматично застосовуємо міграції при старті
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate(); // застосовує всі міграції
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseRouting();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
