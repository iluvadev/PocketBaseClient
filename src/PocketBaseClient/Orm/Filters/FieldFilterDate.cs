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
    /// Filter definitions for Fields of type Date
    /// </summary>
    public class FieldFilterDate : FieldFilter
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="fieldName"></param>
        public FieldFilterDate(string fieldName) : base(fieldName)
        {
        }

        /// <summary>
        /// The Field is Equal to <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand Equal(DateTime value)
            => new($"{FieldName}='{value:yyyy-MM-dd HH:mm:ss}'");

        /// <summary>
        /// The Field is NOT Equal to <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand NotEqual(DateTime value)
            => new($"{FieldName}!='{value:yyyy-MM-dd HH:mm:ss}'");

        /// <summary>
        /// The Field is Greater Than <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand GreaterThan(DateTime value)
            => new($"{FieldName}>'{value:yyyy-MM-dd HH:mm:ss}'");

        /// <summary>
        /// The Field is Greater Than or Equal to <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand GreaterThanOrEqual(DateTime value)
            => new($"{FieldName}>='{value:yyyy-MM-dd HH:mm:ss}'");

        /// <summary>
        /// The Field is Less Than <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand LessThan(DateTime value)
            => new($"{FieldName}<'{value:yyyy-MM-dd HH:mm:ss}'");

        /// <summary>
        /// The Field is Less Than or Equal to <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand LessThanOrEqual(DateTime value)
            => new($"{FieldName}<='{value:yyyy-MM-dd HH:mm:ss}'");

        /// <summary>
        /// The Field is Beetween <paramref name="minValue"/> and <paramref name="maxValue"/>
        /// </summary>
        /// <remarks>The comparison is strictly: Greater than <paramref name="minValue"/> and Less than <paramref name="maxValue"/></remarks>
        /// <param name="minValue">The min value to compare</param>
        /// <param name="maxValue">The max value to compare</param>
        /// <returns></returns>
        public FilterCommand Between(DateTime minValue, DateTime maxValue)
            => new($"{FieldName}>'{minValue:yyyy-MM-dd HH:mm:ss}' && {FieldName}<'{maxValue:yyyy-MM-dd HH:mm:ss}'");
    }
}
