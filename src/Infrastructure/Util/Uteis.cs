namespace Infrastructure.Util
{
    public static class Uteis
    {
        public static string GetEnvironmentVariableWithoutQuotes(string variable)
        {
            return Environment.GetEnvironmentVariable(variable).Trim(new Char[] { '\\', '"'});
        }
    }
}
