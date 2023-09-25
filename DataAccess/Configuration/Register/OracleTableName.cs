namespace DataAccess.Configuration.Register
{
    public static class OracleTableName
    {
        public static string? ToTableName(this IFormattable? table)
        {
            return table?.ToString()?.Replace("]", "").Replace("[", "");
        }
    }
}
