
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
    public partial class CollectionAuthors : CollectionBase<Author>
    {
        /// <inheritdoc />
        public override string Id => "juwfyaf8ba9i3yx";

        /// <inheritdoc />
        public override string Name => "authors";

        /// <inheritdoc />
        public override bool System => false;

        /// <summary> Contructor: The Collection 'authors' </summary>
        /// <param name="context">The DataService for the collection</param>
        internal CollectionAuthors(DataServiceBase context) : base(context) { }

        /// <summary> Query data at PocketBase, defining a Filter over collection 'authors' </summary>
        public CollectionQuery<CollectionAuthors, Author> Filter(Func<Author.Filters, FilterCommand> filter)
            => new CollectionQuery<CollectionAuthors, Author>(this, filter(new Author.Filters()));
    }
}
