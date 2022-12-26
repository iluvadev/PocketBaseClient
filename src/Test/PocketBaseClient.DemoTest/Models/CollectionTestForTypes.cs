
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
    public partial class CollectionTestForTypes : CollectionBase<TestForType>
    {
        /// <inheritdoc />
        public override string Id => "fyyuitlkl66dgyt";

        /// <inheritdoc />
        public override string Name => "test_for_types";

        /// <inheritdoc />
        public override bool System => false;

        public CollectionTestForTypes(DataServiceBase context) : base(context) { }


        /// <summary> Query data at PocketBase, defining a Filter over collection 'test_for_types' </summary>
        public CollectionQuery<CollectionTestForTypes, TestForType> Filter(Func<TestForType.Filters, FilterCommand> filter)
            => new CollectionQuery<CollectionTestForTypes, TestForType>(this, filter(new TestForType.Filters()));

    }
}
