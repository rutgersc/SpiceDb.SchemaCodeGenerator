namespace SpiceDb.SchemaCodeGenerator.Schema;

public record Permission
{
    public string Name { get; set; }

    public string? Comment { get; set; }
}