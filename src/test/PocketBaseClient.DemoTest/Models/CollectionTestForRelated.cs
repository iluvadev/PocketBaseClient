
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
    public partial class CollectionTestForRelated : CollectionBase<TestForRelated>
    {
        public override string Id => "v2ge3yxdn90bhss";
        public override string Name => "test_for_related";
        public override bool System => false;

        public CollectionTestForRelated(DataServiceBase context) : base(context) { }


        public CollectionQuery<CollectionTestForRelated, TestForRelated> Filter(string filterString)
             => new CollectionQuery<CollectionTestForRelated, TestForRelated>(this, FilterQuery.Create(filterString));

        public CollectionQuery<CollectionTestForRelated, TestForRelated> Filter(Func<TestForRelated.Filters, FilterQuery> filter)
            => new CollectionQuery<CollectionTestForRelated, TestForRelated>(this, filter(new TestForRelated.Filters()));

    }
}
