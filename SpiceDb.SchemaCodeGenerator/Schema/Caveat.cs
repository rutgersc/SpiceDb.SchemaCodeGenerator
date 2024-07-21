namespace SpiceDb.SchemaCodeGenerator.Schema;

public record Caveat
{
    public string Name { get; set; }

    public Dictionary<string, string> Parameters { get; set; } = new();

    public string? Comment { get; set; }
}