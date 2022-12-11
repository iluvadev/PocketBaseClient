
// This file was generated automatically for the PocketBase Application demo-test (https://orm-csharp-test.pockethost.io)
//    See CodeGenerationSummary.txt for more details
//
// PocketBaseClient-csharp project: https://github.com/iluvadev/PocketBaseClient-csharp
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using PocketBaseClient.Orm.Filters;
using System.Net.Mail;

namespace PocketBaseClient.DemoTest.Models
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
