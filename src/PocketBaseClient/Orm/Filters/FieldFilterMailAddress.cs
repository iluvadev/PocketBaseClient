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
    /// <summary>
    /// Filter definitions for Fields of type eMail
    /// </summary>
    public class FieldFilterMailAddress : FieldFilterText
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="fieldName"></param>
        public FieldFilterMailAddress(string fieldName) : base(fieldName)
        {
        }

        /// <summary>
        /// The Field is Equal to <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand Equal(MailAddress value)
            => Equal(value.Address);

        /// <summary>
        /// The Field is NOT Equal to <paramref name="value"/>
        /// </summary>
        /// <param name="value">The value to compare</param>
        /// <returns></returns>
        public FilterCommand NotEqual(MailAddress value)
            => NotEqual(value.Address);
    }
}