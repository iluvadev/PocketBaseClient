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
    /// Filter definitions for Fields of type List of Enums
    /// </summary>
    /// <typeparam name="L">The type for the list</typeparam>
    /// <typeparam name="T">The Enum type</typeparam>
    public class FieldFilterEnumList<L, T> : FieldFilter
        where L : FieldBasicList<T>
        where T : struct, IConvertible
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="fieldName"></param>
        public FieldFilterEnumList(string fieldName) : base(fieldName)
        {
        }

        /// <summary>
        /// The Field Contains the <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand Contains(string value)
            => new($"{FieldName}~'%{value}%'");

        /// <summary>
        /// The Field Contains the <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand Contains(T value)
            => Contains(value.GetDescription() ?? value.ToString()!);


        /// <summary>
        /// The Field NOT Contains the <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand NotContains(string value)
            => new($"{FieldName}!~'%{value}%'");

        /// <summary>
        /// The Field NOT Contains the <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand NotContainsl(T value)
            => NotContains(value.GetDescription() ?? value.ToString()!);
    }
}