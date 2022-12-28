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
    public class FieldFilterUri : FieldFilterText
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="fieldName"></param>
        public FieldFilterUri(string fieldName) : base(fieldName)
        {
        }

        public FilterCommand Equal(Uri value)
            => Equal(value.ToString());

        public FilterCommand NotEqual(Uri value)
            => NotEqual(value.ToString());

        public FilterCommand StartsWith(Uri value)
            => StartsWith(value.ToString());

        public FilterCommand NotStartsWith(Uri value)
            => NotStartsWith(value.ToString());
    }
}