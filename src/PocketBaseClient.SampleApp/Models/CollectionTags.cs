
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
    public partial class CollectionTags : CollectionBase<Tag>
    {
        public override string Id => "8zo94eumxnj7ghx";
        public override string Name => "tags";
        public override bool System => false;

        public CollectionTags(DataServiceBase context) : base(context) { }


        public CollectionQuery<CollectionTags, Tag> Filter(string filterString)
             => new CollectionQuery<CollectionTags, Tag>(this, FilterQuery.Create(filterString));

        public CollectionQuery<CollectionTags, Tag> Filter(Func<Tag.Filters, FilterQuery> filter)
            => new CollectionQuery<CollectionTags, Tag>(this, filter(new Tag.Filters()));

    }
}
