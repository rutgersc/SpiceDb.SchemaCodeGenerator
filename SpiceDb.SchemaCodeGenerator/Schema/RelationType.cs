namespace SpiceDb.SchemaCodeGenerator.Schema;

public record RelationType
{
    public string Type { get; set; }

    public string? Namespace { get; set; }

    public string? Relation { get; set; }

    public string? Caveat { get; set; }

    public bool NoRelation =>
        this.Relation == "...";

    public bool IsWildcard =>
        this.Relation == "*";

    public string FullName => string.IsNullOrWhiteSpace(Namespace)
        ? Type
        : $"{Namespace}/{Type}";
}