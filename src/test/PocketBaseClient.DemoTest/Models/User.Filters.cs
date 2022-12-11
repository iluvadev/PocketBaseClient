
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
    public partial class User 
    {
        public class Filters : ItemBaseFilters
        {

            public FilterQuery Name(OperatorText op, string value) => FilterQuery.Create("name", op, value);
            public FilterQuery Url(OperatorText op, Uri value) => FilterQuery.Create("url", op, value);
            public FilterQuery Url(OperatorText op, string value) => FilterQuery.Create("url", op, value);

        }
    }
}
