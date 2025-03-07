﻿namespace SpiceDb.SchemaCodeGenerator.Schema;

public record Definition
{
    private const string AUTO_GENERATED_ID_TYPE = "generated-id-type:";
    private const string DEFAULT_ID_TYPE = "System.Guid";
    private const string DEFAULT_ID_TYPE_TOSTRING = "N";

    public string Name { get; set; }

    public string? Namespace { get; set; }

    public List<Relation> Relations { get; set; } = [];

    public List<Permission> Permissions { get; set; } = [];

    public string? Comment { get; set; }

    public string FullName => string.IsNullOrWhiteSpace(Namespace)
        ? Name
        : $"{Namespace}/{Name}";

    public string IdType => AutoGeneratedIdType?.Type ?? DEFAULT_ID_TYPE;

    public string IdTypeToStringArgument => AutoGeneratedIdType == null
        ? DEFAULT_ID_TYPE_TOSTRING.SurroundWith('"')
        :  AutoGeneratedIdType.Value.ToStringArgument == null
            ? ""
            : AutoGeneratedIdType.Value.ToStringArgument.SurroundWith('"');

    private (string Type, string? ToStringArgument)? AutoGeneratedIdType => Comment
        ?.Split("\n")
        .Where(comment => comment.StartsWith(AUTO_GENERATED_ID_TYPE))
        .Select(comment =>
        {
            var typeArg = comment[AUTO_GENERATED_ID_TYPE.Length..].Split(":");

            return (
                Type: typeArg.First().Trim(),
                ToStringArgument: typeArg.Skip(1).FirstOrDefault()?.Trim());
        })
        .FirstOrDefault();
}