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

        public FilterCommand EqualId(string value)
            => new($"{FieldName}='{value}'");

        public FilterCommand NotEqualId(string value)
            => new($"{FieldName}!='{value}'");

        public FilterCommand Equal(T value)
            => EqualId(value.Id!);

        public FilterCommand NotEqual(T value)
            => NotEqualId(value.Id!);
    }
}