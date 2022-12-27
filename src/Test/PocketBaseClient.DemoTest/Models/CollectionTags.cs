
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
    public partial class CollectionTags : CollectionBase<Tag>
    {
        /// <inheritdoc />
        public override string Id => "8zo94eumxnj7ghx";

        /// <inheritdoc />
        public override string Name => "tags";

        /// <inheritdoc />
        public override bool System => false;

        /// <summary> Contructor: The Collection 'tags' </summary>
        /// <param name="context">The DataService for the collection</param>
        internal CollectionTags(DataServiceBase context) : base(context) { }

        /// <summary> Query data at PocketBase, defining a Filter over collection 'tags' </summary>
        public CollectionQuery<CollectionTags, Tag> Filter(Func<Tag.Filters, FilterCommand> filter)
            => new CollectionQuery<CollectionTags, Tag>(this, filter(new Tag.Filters()));
    }
}
