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
    public class FieldFilterDate : FieldFilter
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="fieldName"></param>
        public FieldFilterDate(string fieldName) : base(fieldName)
        {
        }

        public FilterCommand Equal(DateTime value)
            => new($"{FieldName}='{value:yyyy-MM-dd HH:mm:ss}'");

        public FilterCommand NotEqual(DateTime value)
            => new($"{FieldName}!='{value:yyyy-MM-dd HH:mm:ss}'");

        public FilterCommand GreaterThan(DateTime value)
            => new($"{FieldName}>'{value:yyyy-MM-dd HH:mm:ss}'");

        public FilterCommand GreaterThanOrEqual(DateTime value)
            => new($"{FieldName}>='{value:yyyy-MM-dd HH:mm:ss}'");

        public FilterCommand LessThan(DateTime value)
            => new($"{FieldName}<'{value:yyyy-MM-dd HH:mm:ss}'");

        public FilterCommand LessThanOrEqual(DateTime value)
            => new($"{FieldName}<='{value:yyyy-MM-dd HH:mm:ss}'");

        public FilterCommand Between(DateTime minValue, DateTime maxValue)
            => new($"{FieldName}>'{minValue:yyyy-MM-dd HH:mm:ss}' && {FieldName}<'{maxValue:yyyy-MM-dd HH:mm:ss}'");
    }
}
