namespace SpiceDb.SchemaCodeGenerator.Schema;

public record Relation
{
    public string Name { get; set; }

    public List<RelationType> Types { get; set; } = new();

    public string? Comment { get; set; }
}