// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using PocketBaseClient.Orm.Structures;

namespace PocketBaseClient.Orm.Filters
{
    /// <summary>
    /// Filter definitions for Fields of type List of Relations
    /// </summary>
    /// <typeparam name="L">The type for the list</typeparam>
    /// <typeparam name="T">The Related type</typeparam>
    public class FieldFilterItemList<L, T> : FieldFilter
        where L : FieldItemList<T>
        where T : ItemBase, new()
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="fieldName"></param>
        public FieldFilterItemList(string fieldName) : base(fieldName)
        {
        }

        /// <summary>
        /// The Field Contains a relation to Id <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand ContainsId(string value)
            => new($"{FieldName}~'%{value}%'");

        /// <summary>
        /// The Field NOT Contains a relation to Id <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand NotContainsId(string value)
            => new($"{FieldName}!~'%{value}%'");

        /// <summary>
        /// The Field Contains a relation to <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand Contains(T value)
            => ContainsId(value.Id!);

        /// <summary>
        /// The Field NOT Contains a relation to <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand NotContainsl(T value)
            => NotContainsId(value.Id!);
    }
}