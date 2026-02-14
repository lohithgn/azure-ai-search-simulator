using System.Text;
using System.Text.Json;
using AzureAISearchSimulator.Portal.Models;

namespace AzureAISearchSimulator.Portal.Services;

/// <summary>
/// Typed HTTP client for communicating with the Azure AI Search Simulator REST API.
/// </summary>
public class SearchSimulatorApiClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly JsonSerializerOptions _jsonOptions;

    private string ApiVersion => _configuration["SimulatorApi:ApiVersion"] ?? "2024-07-01";
    public string BaseUrl => _configuration["SimulatorApi:BaseUrl"] ?? "https://localhost:7250";
    public string AdminApiKey => _configuration["SimulatorApi:ApiKey"] ?? "admin-key-12345";
    public string QueryApiKey => _configuration["SimulatorApi:QueryApiKey"] ?? "query-key-67890";

    public SearchSimulatorApiClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };
    }

    private string V(string url) =>
        url.Contains('?') ? $"{url}&api-version={ApiVersion}" : $"{url}?api-version={ApiVersion}";

    // ─── Index Operations ─────────────────────────────────────────

    public async Task<List<SearchIndexModel>> ListIndexesAsync()
    {
        var response = await _httpClient.GetAsync(V("/indexes"));
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<IndexListResponse>(_jsonOptions);
        return result?.Value ?? [];
    }

    public async Task<SearchIndexModel?> GetIndexAsync(string name)
    {
        var response = await _httpClient.GetAsync(V($"/indexes/{name}"));
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<SearchIndexModel>(_jsonOptions);
    }

    public async Task<IndexStatisticsModel?> GetIndexStatsAsync(string name)
    {
        var response = await _httpClient.GetAsync(V($"/indexes/{name}/stats"));
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<IndexStatisticsModel>(_jsonOptions);
    }

    public async Task<bool> CreateIndexAsync(string jsonBody)
    {
        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(V("/indexes"), content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteIndexAsync(string name)
    {
        var response = await _httpClient.DeleteAsync(V($"/indexes/{name}"));
        return response.IsSuccessStatusCode;
    }

    // ─── Document Operations ──────────────────────────────────────

    public async Task<string> SearchDocumentsAsync(string indexName, string jsonBody)
    {
        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(V($"/indexes/{indexName}/docs/search"), content);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<long> CountDocumentsAsync(string indexName)
    {
        var response = await _httpClient.GetAsync(V($"/indexes/{indexName}/docs/$count"));
        if (!response.IsSuccessStatusCode) return 0;
        var text = await response.Content.ReadAsStringAsync();
        return long.TryParse(text, out var count) ? count : 0;
    }

    public async Task<string?> GetDocumentAsync(string indexName, string key)
    {
        var response = await _httpClient.GetAsync(V($"/indexes/{indexName}/docs/{key}"));
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> UploadDocumentsAsync(string indexName, string jsonBody)
    {
        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(V($"/indexes/{indexName}/docs/index"), content);
        return await response.Content.ReadAsStringAsync();
    }

    // ─── Indexer Operations ───────────────────────────────────────

    public async Task<List<IndexerModel>> ListIndexersAsync()
    {
        var response = await _httpClient.GetAsync(V("/indexers"));
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<IndexerListResponse>(_jsonOptions);
        return result?.Value ?? [];
    }

    public async Task<IndexerModel?> GetIndexerAsync(string name)
    {
        var response = await _httpClient.GetAsync(V($"/indexers/{name}"));
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<IndexerModel>(_jsonOptions);
    }

    public async Task<IndexerStatusModel?> GetIndexerStatusAsync(string name)
    {
        var response = await _httpClient.GetAsync(V($"/indexers/{name}/status"));
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<IndexerStatusModel>(_jsonOptions);
    }

    public async Task<bool> RunIndexerAsync(string name)
    {
        var response = await _httpClient.PostAsync(V($"/indexers/{name}/run"), null);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ResetIndexerAsync(string name)
    {
        var response = await _httpClient.PostAsync(V($"/indexers/{name}/reset"), null);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> CreateIndexerAsync(string jsonBody)
    {
        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(V("/indexers"), content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteIndexerAsync(string name)
    {
        var response = await _httpClient.DeleteAsync(V($"/indexers/{name}"));
        return response.IsSuccessStatusCode;
    }

    // ─── Data Source Operations ───────────────────────────────────

    public async Task<List<DataSourceModel>> ListDataSourcesAsync()
    {
        var response = await _httpClient.GetAsync(V("/datasources"));
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<DataSourceListResponse>(_jsonOptions);
        return result?.Value ?? [];
    }

    public async Task<DataSourceModel?> GetDataSourceAsync(string name)
    {
        var response = await _httpClient.GetAsync(V($"/datasources/{name}"));
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<DataSourceModel>(_jsonOptions);
    }

    public async Task<bool> CreateDataSourceAsync(string jsonBody)
    {
        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(V("/datasources"), content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteDataSourceAsync(string name)
    {
        var response = await _httpClient.DeleteAsync(V($"/datasources/{name}"));
        return response.IsSuccessStatusCode;
    }

    // ─── Skillset Operations ─────────────────────────────────────

    public async Task<List<SkillsetModel>> ListSkillsetsAsync()
    {
        var response = await _httpClient.GetAsync(V("/skillsets"));
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<SkillsetListResponse>(_jsonOptions);
        return result?.Value ?? [];
    }

    public async Task<SkillsetModel?> GetSkillsetAsync(string name)
    {
        var response = await _httpClient.GetAsync(V($"/skillsets/{name}"));
        if (!response.IsSuccessStatusCode) return null;
        return await response.Content.ReadFromJsonAsync<SkillsetModel>(_jsonOptions);
    }

    public async Task<bool> CreateSkillsetAsync(string jsonBody)
    {
        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync(V("/skillsets"), content);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteSkillsetAsync(string name)
    {
        var response = await _httpClient.DeleteAsync(V($"/skillsets/{name}"));
        return response.IsSuccessStatusCode;
    }

    // ─── Utility ─────────────────────────────────────────────────

    public async Task<string> GetRawJsonAsync(string path)
    {
        var response = await _httpClient.GetAsync(V(path));
        return await response.Content.ReadAsStringAsync();
    }
}
