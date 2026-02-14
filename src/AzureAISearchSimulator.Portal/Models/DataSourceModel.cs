using System.Text.Json.Serialization;

namespace AzureAISearchSimulator.Portal.Models;

public class DataSourceModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("credentials")]
    public DataSourceCredentials? Credentials { get; set; }

    [JsonPropertyName("container")]
    public DataSourceContainer? Container { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("dataDeletionDetectionPolicy")]
    public object? DataDeletionDetectionPolicy { get; set; }

    [JsonPropertyName("@odata.etag")]
    public string? ODataEtag { get; set; }
}

public class DataSourceCredentials
{
    [JsonPropertyName("connectionString")]
    public string? ConnectionString { get; set; }
}

public class DataSourceContainer
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("query")]
    public string? Query { get; set; }
}

public class DataSourceListResponse
{
    [JsonPropertyName("value")]
    public List<DataSourceModel> Value { get; set; } = [];
}
