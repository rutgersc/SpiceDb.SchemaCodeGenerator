namespace SpiceDb.SchemaCodeGenerator;

internal static class Extensions
{
    public static IEnumerable<TRes> SelectWhereNotNull<T, TRes>(this IEnumerable<T> source, Func<T, TRes?> selector)
        => source
            .Select(selector)
            .Where(item => item != null)
            .Select(item => item!);

    public static string JoinStrings(this IEnumerable<string> str, string seperator) =>
        string.Join(seperator, str);

    public static string SurroundWith(this string? str, char seperator) =>
        $"{seperator}{str}{seperator}";
}
