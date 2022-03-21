using System.Text.Json.Serialization;

namespace Poc.Debezium.Kafka.Consumer;

public record Message
{
    [JsonPropertyName("schema")] public SchemaPrimary Schema { get; set; } = new();
    [JsonPropertyName("payload")] public Payload Payload { get; set; } = new();
}

public record SchemaPrimary
{
    [JsonPropertyName("type")] public string? Type { get; set; }

    [JsonPropertyName("fields")]
    public IReadOnlyCollection<FieldsPrimary> Fields { get; set; } = new List<FieldsPrimary>();

    [JsonPropertyName("optional")] public bool Optional { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
}

public record FieldsPrimary
{
    [JsonPropertyName("type")] public string? Type { get; set; }
    [JsonPropertyName("fields")] public IReadOnlyCollection<FieldChild>? Fields { get; set; } = new List<FieldChild>();
    [JsonPropertyName("optional")] public bool Optional { get; set; }
    [JsonPropertyName("field")] public string? Field { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
}

public record FieldChild
{
    [JsonPropertyName("type")] public string? Type { get; set; }
    [JsonPropertyName("optional")] public bool Optional { get; set; }
    [JsonPropertyName("field")] public string? Field { get; set; }
}

public record Payload
{
    [JsonPropertyName("before")] public Entity? Before { get; set; }
    [JsonPropertyName("after")] public Entity? After { get; set; }

    public record Entity
    {
        [JsonPropertyName("Id")] public int Id { get; set; }
    }
}

public record KeyIdentification
{
    [JsonPropertyName("payload")] public Identification? Payload { get; set; }
}

public record Identification
{
    [JsonPropertyName("Id")] public int Id { get; set; }
}