// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

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
