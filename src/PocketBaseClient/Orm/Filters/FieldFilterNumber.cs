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
    /// Filter definitions for Fields of type Number
    /// </summary>
    public class FieldFilterNumber : FieldFilter
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="fieldName"></param>
        public FieldFilterNumber(string fieldName) : base(fieldName)
        {
        }

        /// <summary>
        /// The Field is Equal to <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand Equal(int value)
            => new($"{FieldName}={value}");

        /// <summary>
        /// The Field is NOT Equal to <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand NotEqual(int value)
            => new($"{FieldName}!={value}");

        /// <summary>
        /// The Field is Greater Than <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand GreaterThan(int value)
            => new($"{FieldName}>{value}");

        /// <summary>
        /// The Field is Greater Than or Equal to <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand GreaterThanOrEqual(int value)
            => new($"{FieldName}>={value}");

        /// <summary>
        /// The Field is Less Than to <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand LessThan(int value)
            => new($"{FieldName}<{value}");

        /// <summary>
        /// The Field is Less Than or Equal to <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand LessThanOrEqual(int value)
            => new($"{FieldName}<={value}");

        /// <summary>
        /// The Field is Beetween <paramref name="minValue"/> and <paramref name="maxValue"/>
        /// </summary>
        /// <remarks>The comparison is strictly: Greater than <paramref name="minValue"/> and Less than <paramref name="maxValue"/></remarks>
        /// <param name="minValue">The min value to compare</param>
        /// <param name="maxValue">The max value to compare</param>
        /// <returns></returns>
        public FilterCommand Between(int minValue, int maxValue)
            => new($"{FieldName}>{minValue} && {FieldName}<{maxValue}");
    }
}
