
// This file was generated automatically on 8/12/2022 0:36:23(UTC) from the PocketBase schema for Application orm-csharp-test (https://orm-csharp-test.pockethost.io)
//
// PocketBaseClient-csharp project: https://github.com/iluvadev/PocketBaseClient-csharp
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using PocketBaseClient.Services;
using PocketBaseClient.SampleApp.Models;

namespace PocketBaseClient.SampleApp.Services
{
    public partial class OrmCsharpTestDataService : DataServiceBase
    {
        #region Collections
        public CollectionUsers UsersCollection { get; }
        public CollectionTestForTypes TestForTypesCollection { get; }
        public CollectionTestForRelated TestForRelatedCollection { get; }
        public CollectionPosts PostsCollection { get; }
        public CollectionAuthors AuthorsCollection { get; }
        public CollectionCategories CategoriesCollection { get; }
        public CollectionTags TagsCollection { get; }

        protected override void RegisterCollections()
        {
            RegisterCollection(typeof(User), UsersCollection);
            RegisterCollection(typeof(TestForTypes), TestForTypesCollection);
            RegisterCollection(typeof(TestForRelated), TestForRelatedCollection);
            RegisterCollection(typeof(Post), PostsCollection);
            RegisterCollection(typeof(Author), AuthorsCollection);
            RegisterCollection(typeof(Category), CategoriesCollection);
            RegisterCollection(typeof(Tag), TagsCollection);
        }
        #endregion Collections

        #region Constructor
        public OrmCsharpTestDataService(PocketBaseClientApplication app) : base(app)
        {
            // Collections
            UsersCollection = new CollectionUsers(this);
            TestForTypesCollection = new CollectionTestForTypes(this);
            TestForRelatedCollection = new CollectionTestForRelated(this);
            PostsCollection = new CollectionPosts(this);
            AuthorsCollection = new CollectionAuthors(this);
            CategoriesCollection = new CollectionCategories(this);
            TagsCollection = new CollectionTags(this);

            RegisterCollections();
        }
        #endregion Constructor
    }
}
