namespace SpiceDb.SchemaCodeGenerator.Schema;

public record Schema
{
    public List<Definition> Definitions { get; set; } = [];

    public List<Caveat> Caveats { get; set; } = [];
}