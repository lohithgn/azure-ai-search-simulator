using AzureAISearchSimulator.Portal.Components;
using AzureAISearchSimulator.Portal.Services;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

// Add Aspire service defaults (OpenTelemetry, health checks, resilience, service discovery)
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add Fluent UI components
builder.Services.AddFluentUIComponents();
builder.Services.AddHttpClient();

// Configure the Search Simulator API client
// When running under Aspire, "https+http://api" resolves via service discovery.
// Falls back to SimulatorApi:BaseUrl from appsettings.json for standalone mode.
builder.Services.AddHttpClient<SearchSimulatorApiClient>(client =>
{
    var baseUrl = builder.Configuration["services:api:https:0"]
        ?? builder.Configuration["services:api:http:0"]
        ?? builder.Configuration["SimulatorApi:BaseUrl"]
        ?? "https://localhost:7250";
    client.BaseAddress = new Uri(baseUrl);
    var apiKey = builder.Configuration["SimulatorApi:ApiKey"] ?? "admin-key-12345";
    client.DefaultRequestHeaders.Add("api-key", apiKey);
});

var app = builder.Build();

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

// Map Aspire default health check endpoints (/health, /alive)
app.MapDefaultEndpoints();

app.Run();
