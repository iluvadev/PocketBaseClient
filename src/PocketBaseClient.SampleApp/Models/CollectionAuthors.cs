
// This file was generated automatically on 8/12/2022 21:42:16(UTC) from the PocketBase schema for Application orm-csharp-test (https://orm-csharp-test.pockethost.io)
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

namespace PocketBaseClient.SampleApp.Models
{
    public partial class CollectionAuthors : CollectionBase<Author>
    {
        public override string Id => "juwfyaf8ba9i3yx";
        public override string Name => "authors";
        public override bool System => false;

        public CollectionAuthors(DataServiceBase context) : base(context) { }


        public CollectionQuery<CollectionAuthors, Author> Filter(string filterString)
             => new CollectionQuery<CollectionAuthors, Author>(this, FilterQuery.Create(filterString));

        public CollectionQuery<CollectionAuthors, Author> Filter(Func<Author.Filters, FilterQuery> filter)
            => new CollectionQuery<CollectionAuthors, Author>(this, filter(new Author.Filters()));

    }
}
