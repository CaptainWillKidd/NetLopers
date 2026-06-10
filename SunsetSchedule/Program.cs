using SunsetSchedule.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using SunsetSchedule.Data;
using SunsetSchedule.Services;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine($"ENV: {builder.Environment.EnvironmentName}");

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<ActivityService>();
builder.Services.AddScoped<ScheduledActivityService>();

var app = builder.Build();

Console.WriteLine($"ENVIRONMENT: {app.Environment.EnvironmentName}");

// Debug DB provider (optional)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    Console.WriteLine($"DB Provider: {db.Database.ProviderName}");
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


// ❌ REMOVED:
// - EnsureCreated()
// - Migrate()

// ✅ OPTIONAL SAFE MIGRATION PATTERN (ONLY if you want automatic migrations later)
// NOTE: still better in CI/CD for Render, not runtime
/*
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    if (db.Database.GetPendingMigrations().Any())
    {
        db.Database.Migrate();
    }
}
*/

// ✅ SEEDING (safe version)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    // Only seed in development (prevents production duplication issues)
    if (app.Environment.IsDevelopment())
    {
        try
        {
            DbSeeder.Seed(db);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Seeding failed: " + ex.Message);
        }
    }
}

Console.WriteLine("CONNECTION STRING:");
Console.WriteLine(builder.Configuration.GetConnectionString("DefaultConnection"));

app.Run();