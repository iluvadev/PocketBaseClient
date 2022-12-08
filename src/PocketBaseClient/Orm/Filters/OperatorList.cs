namespace PocketBaseClient.Orm.Filters
{
    public enum OperatorList
    {
        Contains,
        NotContains,
    }

    internal static class OperatorListExtensions
    {
        public static string OperatorString(this OperatorList op)
            => op switch
            {
                OperatorList.Contains => "~",
                OperatorList.NotContains => "!~",
                _ => "",
            };
    }
}
