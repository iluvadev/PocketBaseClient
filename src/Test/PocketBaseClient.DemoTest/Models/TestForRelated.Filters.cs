
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

namespace PocketBaseClient.DemoTest.Models
{
    public partial class TestForRelated
    {
        public class Filters : ItemBaseFilters
        {

            /// <summary> Gets a Filter to Query data over the 'number_unique' field in PocketBase </summary>
            public FieldFilterNumber NumberUnique => new FieldFilterNumber("number_unique");

            /// <summary> Gets a Filter to Query data over the 'number_nonempty' field in PocketBase </summary>
            public FieldFilterNumber NumberNonempty => new FieldFilterNumber("number_nonempty");

            /// <summary> Gets a Filter to Query data over the 'number_nonempty_unique' field in PocketBase </summary>
            public FieldFilterNumber NumberNonemptyUnique => new FieldFilterNumber("number_nonempty_unique");


        }
    }
}
