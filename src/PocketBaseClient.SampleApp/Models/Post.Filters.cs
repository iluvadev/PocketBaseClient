
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
    public partial class Post 
    {
        public class Filters : ItemBaseFilters
        {

            public FilterQuery Title(OperatorText op, string value) => FilterQuery.Create("title", op, value);
            public FilterQuery Summary(OperatorText op, string value) => FilterQuery.Create("summary", op, value);
            public FilterQuery Content(OperatorText op, string value) => FilterQuery.Create("content", op, value);
            public FilterQuery Published(OperatorNumeric op, DateTime value) => FilterQuery.Create("published", op, value);

        }
    }
}
