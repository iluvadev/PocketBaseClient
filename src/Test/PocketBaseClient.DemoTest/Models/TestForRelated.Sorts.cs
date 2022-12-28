
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
        public class Sorts : ItemBaseSorts
        {

            /// <summary>Makes a SortCommand to Order by the 'number_unique' field</summary>
            public SortCommand NumberUnique => new SortCommand("number_unique");

            /// <summary>Makes a SortCommand to Order by the 'number_nonempty' field</summary>
            public SortCommand NumberNonempty => new SortCommand("number_nonempty");

            /// <summary>Makes a SortCommand to Order by the 'number_nonempty_unique' field</summary>
            public SortCommand NumberNonemptyUnique => new SortCommand("number_nonempty_unique");


        }
    }
}
