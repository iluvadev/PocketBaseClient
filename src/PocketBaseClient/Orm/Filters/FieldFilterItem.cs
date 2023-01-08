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
    /// Filter definitions for Fields of type Relation
    /// </summary>
    /// <typeparam name="T">The related type</typeparam>
    public class FieldFilterItem<T> : FieldFilter
        where T : ItemBase
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="fieldName"></param>
        public FieldFilterItem(string fieldName) : base(fieldName)
        {
        }

        /// <summary>
        /// The Field is related to Id Equal to <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand EqualId(string value)
            => new($"{FieldName}='{value}'");

        /// <summary>
        /// The Field is related to Id NOT Equal to <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand NotEqualId(string value)
            => new($"{FieldName}!='{value}'");

        /// <summary>
        /// The Field is Equal to <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand Equal(T value)
            => EqualId(value.Id!);

        /// <summary>
        /// The Field is NOT Equal to <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand NotEqual(T value)
            => NotEqualId(value.Id!);
    }
}