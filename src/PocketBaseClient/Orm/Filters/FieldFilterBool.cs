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
    public class FieldFilterBool : FieldFilter
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="fieldName"></param>
        public FieldFilterBool(string fieldName) : base(fieldName)
        {
        }

        public FilterCommand Equal(bool value)
            => new($"{FieldName}={value}");

        public FilterCommand NotEqual(bool value)
            => new($"{FieldName}!={value}");

        public FilterCommand IsTrue()
            => Equal(true);

        public FilterCommand IsFalse()
            => Equal(false);
    }
}
