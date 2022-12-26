
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
    public partial class Author 
    {
        public class Filters : ItemBaseFilters
        {

            /// <summary>Makes a Filter to Query data over the 'name' field</summary>
            public FieldFilterText Name => new FieldFilterText("name");
            /// <summary>Makes a Filter to Query data over the 'email' field</summary>
            public FieldFilterMailAddress Email => new FieldFilterMailAddress("email");
            /// <summary>Makes a Filter to Query data over the 'url' field</summary>
            public FieldFilterUri Url => new FieldFilterUri("url");
            /// <summary>Makes a Filter to Query data over the 'profile' field</summary>
            public FieldFilterText Profile => new FieldFilterText("profile");

        }
    }
}
