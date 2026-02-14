using System.Text.Json.Serialization;

namespace AzureAISearchSimulator.Portal.Models;

public class SkillsetModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("skills")]
    public List<SkillModel> Skills { get; set; } = [];

    [JsonPropertyName("@odata.etag")]
    public string? ODataEtag { get; set; }
}

public class SkillModel
{
    [JsonPropertyName("@odata.type")]
    public string ODataType { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("context")]
    public string? Context { get; set; }

    [JsonPropertyName("inputs")]
    public List<SkillIO>? Inputs { get; set; }

    [JsonPropertyName("outputs")]
    public List<SkillIO>? Outputs { get; set; }
}

public class SkillIO
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("source")]
    public string? Source { get; set; }

    [JsonPropertyName("targetName")]
    public string? TargetName { get; set; }
}

public class SkillsetListResponse
{
    [JsonPropertyName("value")]
    public List<SkillsetModel> Value { get; set; } = [];
}
