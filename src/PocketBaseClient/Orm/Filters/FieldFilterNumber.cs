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
    public class FieldFilterNumber : FieldFilter
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="fieldName"></param>
        public FieldFilterNumber(string fieldName) : base(fieldName)
        {
        }

        public FilterCommand Equal(int value)
            => new($"{FieldName}={value}");

        public FilterCommand NotEqual(int value)
            => new($"{FieldName}!={value}");

        public FilterCommand GreaterThan(int value)
            => new($"{FieldName}>{value}");

        public FilterCommand GreaterThanOrEqual(int value)
            => new($"{FieldName}>={value}");

        public FilterCommand LessThan(int value)
            => new($"{FieldName}<{value}");

        public FilterCommand LessThanOrEqual(int value)
            => new($"{FieldName}<={value}");

        public FilterCommand Between(int minValue, int maxValue)
            => new($"{FieldName}>{minValue} && {FieldName}<{maxValue}");
    }
}
