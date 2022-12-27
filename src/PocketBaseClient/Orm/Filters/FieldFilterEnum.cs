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
    public class FieldFilterEnum<T> : FieldFilterText
        where T : struct, IConvertible
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="fieldName"></param>
        public FieldFilterEnum(string fieldName) : base(fieldName)
        {
        }

        public FilterCommand Equal(T value)
            => Equal(value.GetDescription() ?? value.ToString()!);

        public FilterCommand NotEqual(T value)
            => NotEqual(value.GetDescription() ?? value.ToString()!);
    }
}