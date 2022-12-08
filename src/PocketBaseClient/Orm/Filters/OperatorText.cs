namespace PocketBaseClient.Orm.Filters
{
    public enum OperatorText
    {
        Equal,
        NotEqual,

        Like,
        NotLike,
    }

    internal static class OperatorTextExtensions
    {
        public static string OperatorString(this OperatorText op)
            => op switch
            {
                OperatorText.Equal => "=",
                OperatorText.NotEqual => "!=",
                OperatorText.Like => "~",
                OperatorText.NotLike => "!~",
                _ => "=",
            };

    }

}
