
// This file was generated automatically on 8/12/2022 21:42:16(UTC) from the PocketBase schema for Application orm-csharp-test (https://orm-csharp-test.pockethost.io)
//
// PocketBaseClient-csharp project: https://github.com/iluvadev/PocketBaseClient-csharp
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using PocketBaseClient.Orm.Filters;
using System.Net.Mail;

namespace PocketBaseClient.SampleApp.Models
{
    public partial class Author 
    {
        public class Filters : ItemBaseFilters
        {

            public FilterQuery Name(OperatorText op, string value) => FilterQuery.Create("name", op, value);
            public FilterQuery Email(OperatorText op, MailAddress value) => FilterQuery.Create("email", op, value);
            public FilterQuery Email(OperatorText op, string value) => FilterQuery.Create("email", op, value);
            public FilterQuery Url(OperatorText op, Uri value) => FilterQuery.Create("url", op, value);
            public FilterQuery Url(OperatorText op, string value) => FilterQuery.Create("url", op, value);
            public FilterQuery Profile(OperatorText op, string value) => FilterQuery.Create("profile", op, value);

        }
    }
}
