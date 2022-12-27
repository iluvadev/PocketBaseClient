
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

            /// <summary> Gets a Filter to Query data over the 'name' field in PocketBase </summary>
            public FieldFilterText Name => new FieldFilterText("name");

            /// <summary> Gets a Filter to Query data over the 'avatar' field in PocketBase </summary>
            public FieldFilterText Avatar => new FieldFilterText("avatar");

            /// <summary> Gets a Filter to Query data over the 'url' field in PocketBase </summary>
            public FieldFilterUri Url => new FieldFilterUri("url");


        }
    }
}
