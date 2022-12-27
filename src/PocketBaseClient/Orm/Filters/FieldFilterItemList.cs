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

        public FilterCommand ContainsId(string value)
            => new($"{FieldName}~'%{value}%'");

        public FilterCommand NotContainsId(string value)
            => new($"{FieldName}!~'%{value}%'");

        public FilterCommand Contains(T value)
            => ContainsId(value.Id!);

        public FilterCommand NotContainsl(T value)
            => NotContainsId(value.Id!);
    }
}