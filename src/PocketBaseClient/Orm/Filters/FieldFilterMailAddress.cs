// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using System.Net.Mail;

namespace PocketBaseClient.Orm.Filters
{
    public class FieldFilterMailAddress : FieldFilterText
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="fieldName"></param>
        public FieldFilterMailAddress(string fieldName) : base(fieldName)
        {
        }

        public FilterCommand Equal(MailAddress value)
            => Equal(value.Address);

        public FilterCommand NotEqual(MailAddress value)
            => NotEqual(value.Address);
    }
}