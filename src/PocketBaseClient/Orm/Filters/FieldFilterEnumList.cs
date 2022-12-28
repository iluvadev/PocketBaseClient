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

        public FilterCommand Contains(string value)
            => new($"{FieldName}~'%{value}%'");

        public FilterCommand NotContains(string value)
            => new($"{FieldName}!~'%{value}%'");

        public FilterCommand Contains(T value)
            => Contains(value.GetDescription() ?? value.ToString()!);

        public FilterCommand NotContainsl(T value)
            => NotContains(value.GetDescription() ?? value.ToString()!);
    }
}