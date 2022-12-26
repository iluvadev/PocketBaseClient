
// This file was generated automatically for the PocketBase Application demo-test (https://orm-csharp-test.pockethost.io)
//    See CodeGenerationSummary.txt for more details
//
// PocketBaseClient-csharp project: https://github.com/iluvadev/PocketBaseClient-csharp
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using PocketBaseClient.Orm.Structures;

namespace PocketBaseClient.DemoTest.Models
{
    public partial class TestForType
    {
        public class SelectMultipleList : FieldBasicList<SelectMultipleEnum>
        {
            public SelectMultipleList() : this(null) { }

            public SelectMultipleList(TestForType? testForType) : base(testForType, "SelectMultiple", "8dks1xfy", 10) { }
        }
    }
}
