
// This file was generated automatically for the PocketBase Application demo-test (https://orm-csharp-test.pockethost.io)
//    See CodeGenerationSummary.txt for more details
//
// PocketBaseClient-csharp project: https://github.com/iluvadev/PocketBaseClient-csharp
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using PocketBaseClient.Orm;
using PocketBaseClient.Orm.Filters;
using PocketBaseClient.Services;

namespace PocketBaseClient.DemoTest.Models
{
    public partial class CollectionTestForTypes : CollectionBase<TestForTypes>
    {
        public override string Id => "fyyuitlkl66dgyt";
        public override string Name => "test_for_types";
        public override bool System => false;

        public CollectionTestForTypes(DataServiceBase context) : base(context) { }


        public CollectionQuery<CollectionTestForTypes, TestForTypes> Filter(string filterString)
             => new CollectionQuery<CollectionTestForTypes, TestForTypes>(this, FilterQuery.Create(filterString));

        public CollectionQuery<CollectionTestForTypes, TestForTypes> Filter(Func<TestForTypes.Filters, FilterQuery> filter)
            => new CollectionQuery<CollectionTestForTypes, TestForTypes>(this, filter(new TestForTypes.Filters()));

    }
}
