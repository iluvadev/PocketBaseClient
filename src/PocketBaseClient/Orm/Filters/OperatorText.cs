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
    /// <summary> Operators for texts </summary>
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
