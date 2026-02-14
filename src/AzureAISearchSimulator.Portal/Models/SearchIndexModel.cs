using System.Text.Json.Serialization;

namespace AzureAISearchSimulator.Portal.Models;

public class SearchIndexModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("fields")]
    public List<SearchFieldModel> Fields { get; set; } = [];

    [JsonPropertyName("suggesters")]
    public List<SuggesterModel>? Suggesters { get; set; }

    [JsonPropertyName("vectorSearch")]
    public object? VectorSearch { get; set; }

    [JsonPropertyName("scoringProfiles")]
    public List<object>? ScoringProfiles { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("@odata.etag")]
    public string? ODataEtag { get; set; }
}

public class SearchFieldModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; set; } = "Edm.String";

    [JsonPropertyName("key")]
    public bool Key { get; set; }

    [JsonPropertyName("searchable")]
    public bool? Searchable { get; set; }

    [JsonPropertyName("filterable")]
    public bool? Filterable { get; set; }

    [JsonPropertyName("sortable")]
    public bool? Sortable { get; set; }

    [JsonPropertyName("facetable")]
    public bool? Facetable { get; set; }

    [JsonPropertyName("retrievable")]
    public bool? Retrievable { get; set; }

    [JsonPropertyName("dimensions")]
    public int? Dimensions { get; set; }

    [JsonPropertyName("vectorSearchProfile")]
    public string? VectorSearchProfile { get; set; }

    [JsonPropertyName("fields")]
    public List<SearchFieldModel>? Fields { get; set; }
}

public class SuggesterModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("searchMode")]
    public string SearchMode { get; set; } = "analyzingInfixMatching";

    [JsonPropertyName("sourceFields")]
    public List<string> SourceFields { get; set; } = [];
}

public class IndexStatisticsModel
{
    [JsonPropertyName("documentCount")]
    public long DocumentCount { get; set; }

    [JsonPropertyName("storageSize")]
    public long StorageSize { get; set; }

    [JsonPropertyName("vectorIndexSize")]
    public long VectorIndexSize { get; set; }
}

public class IndexListResponse
{
    [JsonPropertyName("value")]
    public List<SearchIndexModel> Value { get; set; } = [];
}
