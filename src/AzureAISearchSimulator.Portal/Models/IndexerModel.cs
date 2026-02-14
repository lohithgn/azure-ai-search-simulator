using System.Text.Json.Serialization;

namespace AzureAISearchSimulator.Portal.Models;

public class IndexerModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("dataSourceName")]
    public string? DataSourceName { get; set; }

    [JsonPropertyName("targetIndexName")]
    public string? TargetIndexName { get; set; }

    [JsonPropertyName("skillsetName")]
    public string? SkillsetName { get; set; }

    [JsonPropertyName("schedule")]
    public IndexerSchedule? Schedule { get; set; }

    [JsonPropertyName("fieldMappings")]
    public List<FieldMapping>? FieldMappings { get; set; }

    [JsonPropertyName("outputFieldMappings")]
    public List<FieldMapping>? OutputFieldMappings { get; set; }

    [JsonPropertyName("parameters")]
    public object? Parameters { get; set; }

    [JsonPropertyName("@odata.etag")]
    public string? ODataEtag { get; set; }
}

public class IndexerSchedule
{
    [JsonPropertyName("interval")]
    public string? Interval { get; set; }

    [JsonPropertyName("startTime")]
    public string? StartTime { get; set; }
}

public class FieldMapping
{
    [JsonPropertyName("sourceFieldName")]
    public string SourceFieldName { get; set; } = string.Empty;

    [JsonPropertyName("targetFieldName")]
    public string? TargetFieldName { get; set; }

    [JsonPropertyName("mappingFunction")]
    public object? MappingFunction { get; set; }
}

public class IndexerStatusModel
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = "unknown";

    [JsonPropertyName("lastResult")]
    public IndexerExecutionResult? LastResult { get; set; }

    [JsonPropertyName("executionHistory")]
    public List<IndexerExecutionResult>? ExecutionHistory { get; set; }
}

public class IndexerExecutionResult
{
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    [JsonPropertyName("startTime")]
    public string? StartTime { get; set; }

    [JsonPropertyName("endTime")]
    public string? EndTime { get; set; }

    [JsonPropertyName("itemsProcessed")]
    public int ItemsProcessed { get; set; }

    [JsonPropertyName("itemsFailed")]
    public int ItemsFailed { get; set; }

    [JsonPropertyName("errors")]
    public List<object>? Errors { get; set; }

    [JsonPropertyName("warnings")]
    public List<object>? Warnings { get; set; }
}

public class IndexerListResponse
{
    [JsonPropertyName("value")]
    public List<IndexerModel> Value { get; set; } = [];
}
