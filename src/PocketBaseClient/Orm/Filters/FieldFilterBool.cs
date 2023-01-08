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
    /// <summary>
    /// Filter definitions for Fields of type Bool
    /// </summary>
    public class FieldFilterBool : FieldFilter
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="fieldName"></param>
        public FieldFilterBool(string fieldName) : base(fieldName)
        {
        }

        /// <summary>
        /// The Field is Equal to <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand Equal(bool value)
            => new($"{FieldName}={value}");

        /// <summary>
        /// The Field is NOT Equal to <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand NotEqual(bool value)
            => new($"{FieldName}!={value}");

        /// <summary>
        /// The Field is TRUE
        /// </summary>
        /// <returns></returns>
        public FilterCommand IsTrue()
            => Equal(true);

        /// <summary>
        /// The Field is FALSE
        /// </summary>
        /// <returns></returns>
        public FilterCommand IsFalse()
            => Equal(false);
    }
}
