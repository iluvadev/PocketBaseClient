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
    public class FieldFilterText : FieldFilter
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="fieldName"></param>
        public FieldFilterText(string fieldName) : base(fieldName)
        {
        }

        public FilterCommand Equal(string value)
            => new($"{FieldName}='{value}'");

        public FilterCommand NotEqual(string value)
            => new($"{FieldName}!='{value}'");

        public FilterCommand Like(string value) 
            => new($"{FieldName}~'{value}'");

        public FilterCommand NotLike(string value) 
            => new($"{FieldName}!~'{value}'");

        public FilterCommand StartsWith(string value)
            => new($"{FieldName}~'{value}%'");

        public FilterCommand NotStartsWith(string value)
            => new($"{FieldName}!~'{value}%'");

        public FilterCommand EndsWith(string value)
            => new($"{FieldName}~'%{value}'");

        public FilterCommand NotEndsWith(string value) 
            => new($"{FieldName}!~'%{value}'");

        public FilterCommand Contains(string value)
            => new($"{FieldName}~'%{value}%'");
        public FilterCommand NotContains(string value)
            => new($"{FieldName}!~'%{value}%'");
    }
}
