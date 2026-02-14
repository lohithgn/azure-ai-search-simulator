using System.Text.Json.Serialization;

namespace AzureAISearchSimulator.Portal.Models;

public class SearchResultModel
{
    [JsonPropertyName("@odata.context")]
    public string? ODataContext { get; set; }

    [JsonPropertyName("@odata.count")]
    public long? ODataCount { get; set; }

    [JsonPropertyName("@search.facets")]
    public Dictionary<string, List<FacetValue>>? SearchFacets { get; set; }

    [JsonPropertyName("@search.debug")]
    public object? SearchDebug { get; set; }

    [JsonPropertyName("value")]
    public List<Dictionary<string, object>>? Value { get; set; }
}

public class FacetValue
{
    [JsonPropertyName("value")]
    public object? Value { get; set; }

    [JsonPropertyName("count")]
    public long Count { get; set; }
}

public class DocumentUploadRequest
{
    [JsonPropertyName("value")]
    public List<Dictionary<string, object>> Value { get; set; } = [];
}

public class DocumentUploadResponse
{
    [JsonPropertyName("value")]
    public List<DocumentUploadResult> Value { get; set; } = [];
}

public class DocumentUploadResult
{
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    [JsonPropertyName("status")]
    public bool Status { get; set; }

    [JsonPropertyName("errorMessage")]
    public string? ErrorMessage { get; set; }

    [JsonPropertyName("statusCode")]
    public int StatusCode { get; set; }
}
