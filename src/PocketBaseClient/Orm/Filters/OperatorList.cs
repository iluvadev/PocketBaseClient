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
