
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
    public partial class CollectionPosts : CollectionBase<Post>
    {
        public override string Id => "ev0tqm560oseykn";
        public override string Name => "posts";
        public override bool System => false;

        public CollectionPosts(DataServiceBase context) : base(context) { }


        public CollectionQuery<CollectionPosts, Post> Filter(string filterString)
             => new CollectionQuery<CollectionPosts, Post>(this, FilterQuery.Create(filterString));

        public CollectionQuery<CollectionPosts, Post> Filter(Func<Post.Filters, FilterQuery> filter)
            => new CollectionQuery<CollectionPosts, Post>(this, filter(new Post.Filters()));

    }
}
