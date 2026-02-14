using AzureAISearchSimulator.Portal.Components;
using AzureAISearchSimulator.Portal.Services;
using Microsoft.FluentUI.AspNetCore.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add Fluent UI components
builder.Services.AddFluentUIComponents();
builder.Services.AddHttpClient();

// Configure the Search Simulator API client
builder.Services.AddHttpClient<SearchSimulatorApiClient>(client =>
{
    var baseUrl = builder.Configuration["SimulatorApi:BaseUrl"] ?? "https://localhost:7250";
    client.BaseAddress = new Uri(baseUrl);
    var apiKey = builder.Configuration["SimulatorApi:ApiKey"] ?? "admin-key-12345";
    client.DefaultRequestHeaders.Add("api-key", apiKey);
})
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
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

app.Run();
