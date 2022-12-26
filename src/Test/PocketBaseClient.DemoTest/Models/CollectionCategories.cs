
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
    public partial class CollectionCategories : CollectionBase<Category>
    {
        /// <inheritdoc />
        public override string Id => "fgj34p0gy01o41e";

        /// <inheritdoc />
        public override string Name => "categories";

        /// <inheritdoc />
        public override bool System => false;

        public CollectionCategories(DataServiceBase context) : base(context) { }


        /// <summary> Query data at PocketBase, defining a Filter over collection 'categories' </summary>
        public CollectionQuery<CollectionCategories, Category> Filter(Func<Category.Filters, FilterCommand> filter)
            => new CollectionQuery<CollectionCategories, Category>(this, filter(new Category.Filters()));

    }
}
