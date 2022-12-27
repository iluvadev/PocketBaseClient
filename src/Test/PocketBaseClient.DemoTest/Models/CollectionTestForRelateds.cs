
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
    public partial class CollectionTestForRelateds : CollectionBase<TestForRelated>
    {
        /// <inheritdoc />
        public override string Id => "v2ge3yxdn90bhss";

        /// <inheritdoc />
        public override string Name => "test_for_related";

        /// <inheritdoc />
        public override bool System => false;

        /// <summary> Contructor: The Collection 'test_for_related' </summary>
        /// <param name="context">The DataService for the collection</param>
        internal CollectionTestForRelateds(DataServiceBase context) : base(context) { }

        /// <summary> Query data at PocketBase, defining a Filter over collection 'test_for_related' </summary>
        public CollectionQuery<CollectionTestForRelateds, TestForRelated> Filter(Func<TestForRelated.Filters, FilterCommand> filter)
            => new CollectionQuery<CollectionTestForRelateds, TestForRelated>(this, filter(new TestForRelated.Filters()));
    }
}
