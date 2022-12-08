namespace PocketBaseClient.Orm.Filters
{
    public enum OperatorNumeric
    {
        Equal,
        NotEqual,

        GreaterThan,
        GreaterThanOrEqual,

        LessThan,
        LessThanOrEqual,
    }

    internal static class OperatorNumericExtensions
    {
        public static string OperatorString(this OperatorNumeric op)
            => op switch
            {
                OperatorNumeric.Equal => "=",
                OperatorNumeric.NotEqual => "!=",
                OperatorNumeric.GreaterThan => ">",
                OperatorNumeric.GreaterThanOrEqual => ">=",
                OperatorNumeric.LessThan => "<",
                OperatorNumeric.LessThanOrEqual => "<=",
                _ => "",
            };
    }
}
