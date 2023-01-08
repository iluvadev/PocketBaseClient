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
    /// Filter definitions for Fields of type Text
    /// </summary>
    public class FieldFilterText : FieldFilter
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="fieldName"></param>
        public FieldFilterText(string fieldName) : base(fieldName)
        {
        }

        /// <summary>
        /// The Field is Equal to <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand Equal(string value)
            => new($"{FieldName}='{value}'");

        /// <summary>
        /// The Field is NOT Equal to <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand NotEqual(string value)
            => new($"{FieldName}!='{value}'");

        /// <summary>
        /// The Field is Like <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand Like(string value) 
            => new($"{FieldName}~'{value}'");

        /// <summary>
        /// The Field is NOT Like <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand NotLike(string value) 
            => new($"{FieldName}!~'{value}'");

        /// <summary>
        /// The Field Starts With <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand StartsWith(string value)
            => new($"{FieldName}~'{value}%'");

        /// <summary>
        /// The Field do NOT Starts With <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand NotStartsWith(string value)
            => new($"{FieldName}!~'{value}%'");

        /// <summary>
        /// The Field Ends With <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand EndsWith(string value)
            => new($"{FieldName}~'%{value}'");

        /// <summary>
        /// The Field do NOT Ends With <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand NotEndsWith(string value) 
            => new($"{FieldName}!~'%{value}'");

        /// <summary>
        /// The Field Contains <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand Contains(string value)
            => new($"{FieldName}~'%{value}%'");

        /// <summary>
        /// The Field NOT Contains <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand NotContains(string value)
            => new($"{FieldName}!~'%{value}%'");
    }
}
