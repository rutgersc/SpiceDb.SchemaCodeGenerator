using System.Diagnostics;
using System.Text.Json;
using SpiceDb.SchemaCodeGenerator;
using SpiceDb.SchemaCodeGenerator.Schema;

var spice2jsonPath = "spice2json";
var schemaName = "schema";
var schemaPath = $"../../../{schemaName}.zed";
var schemaDir = Path.GetDirectoryName(schemaPath);

var schemaText = File.ReadAllText(schemaPath);
var schemaJson = GetSchemaJsonText(spice2jsonPath, schemaText);
File.WriteAllText($"{schemaDir}/{schemaName}.json", schemaJson);

Schema schema;

try
{
    schema = JsonSerializer.Deserialize<Schema>(schemaJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
}
catch (JsonException ex)
{
    throw new Exception($"Failed to deserialize schema JSON. See {Path.GetFullPath(spice2jsonPath)}.", ex);
}

var generatedCode = SchemaCodeGenerator.GenerateFullSchema(schema);
File.WriteAllText($"{schemaDir}/{schemaName}.g.cs", generatedCode);

return;

static string GetSchemaJsonText(string spice2JsonPath, string schemaText)
{
    using var p = new Process();
    p.StartInfo = new ProcessStartInfo(spice2JsonPath)
    {
        Arguments = "-s",
        RedirectStandardInput = true,
        RedirectStandardOutput = true,
        UseShellExecute = false
    };
    p.Start();

    p.StandardInput.Write(schemaText);
    p.StandardInput.Close();
    var jsonText = p.StandardOutput.ReadToEnd();

    p.WaitForExit();

    if (p.ExitCode != 0)
    {
        throw new Exception($"spice2json failed with exit code: {p.ExitCode}");
    }

    return jsonText;
}
