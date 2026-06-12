using SunsetSchedule.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using SunsetSchedule.Data;
using SunsetSchedule.Services;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine($"ENV: {builder.Environment.EnvironmentName}");

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

/* =========================
   BLABOR SERVER (CORE)
========================= */
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

/* =========================
   APPLICATION SERVICES
========================= */
builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<ActivityService>();
builder.Services.AddScoped<ScheduledActivityService>();
builder.Services.AddScoped<AuthService>();

/* =========================
   AUTH (MANUAL SESSION AUTH)
========================= */
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthStateProvider>();

builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<AuthStateProvider>());

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

Console.WriteLine($"ENVIRONMENT: {app.Environment.EnvironmentName}");

/* =========================
   ERROR HANDLING
========================= */
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

/* ❌ IMPORTANT: DO NOT USE COOKIE AUTH MIDDLEWARE */
/* REMOVE ANY OF THESE IF THEY EVER COME BACK:
   app.UseAuthentication();
   builder.Services.AddAuthentication();
   AddCookie();
*/

app.UseAuthorization(); // harmless, but not strictly required for manual auth

app.UseAntiforgery();

/* =========================
   APP MAPPING
========================= */
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

/* =========================
   SEEDING
========================= */
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

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

app.Run();