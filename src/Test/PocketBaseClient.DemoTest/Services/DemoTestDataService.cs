
// This file was generated automatically for the PocketBase Application demo-test (https://orm-csharp-test.pockethost.io)
//    See CodeGenerationSummary.txt for more details
//
// PocketBaseClient-csharp project: https://github.com/iluvadev/PocketBaseClient-csharp
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using PocketBaseClient;
using PocketBaseClient.Services;
using PocketBaseClient.DemoTest.Models;

namespace PocketBaseClient.DemoTest.Services
{
    public partial class DemoTestDataService : DataServiceBase
    {
        #region Collections
        /// <summary> Collection 'users' in PocketBase </summary>
        public CollectionUsers UsersCollection { get; }
        /// <summary> Collection 'test_for_types' in PocketBase </summary>
        public CollectionTestForTypes TestForTypesCollection { get; }
        /// <summary> Collection 'test_for_related' in PocketBase </summary>
        public CollectionTestForRelateds TestForRelatedsCollection { get; }
        /// <summary> Collection 'posts' in PocketBase </summary>
        public CollectionPosts PostsCollection { get; }
        /// <summary> Collection 'authors' in PocketBase </summary>
        public CollectionAuthors AuthorsCollection { get; }
        /// <summary> Collection 'categories' in PocketBase </summary>
        public CollectionCategories CategoriesCollection { get; }
        /// <summary> Collection 'tags' in PocketBase </summary>
        public CollectionTags TagsCollection { get; }

        /// <inheritdoc />
        protected override void RegisterCollections()
        {
            RegisterCollection(typeof(PocketBaseClient.DemoTest.Models.User), UsersCollection);
            RegisterCollection(typeof(PocketBaseClient.DemoTest.Models.TestForType), TestForTypesCollection);
            RegisterCollection(typeof(PocketBaseClient.DemoTest.Models.TestForRelated), TestForRelatedsCollection);
            RegisterCollection(typeof(PocketBaseClient.DemoTest.Models.Post), PostsCollection);
            RegisterCollection(typeof(PocketBaseClient.DemoTest.Models.Author), AuthorsCollection);
            RegisterCollection(typeof(PocketBaseClient.DemoTest.Models.Category), CategoriesCollection);
            RegisterCollection(typeof(PocketBaseClient.DemoTest.Models.Tag), TagsCollection);
        }
        #endregion Collections

        #region Constructor
        public DemoTestDataService(PocketBaseClientApplication app) : base(app)
        {
            // Collections
            UsersCollection = new CollectionUsers(this);
            TestForTypesCollection = new CollectionTestForTypes(this);
            TestForRelatedsCollection = new CollectionTestForRelateds(this);
            PostsCollection = new CollectionPosts(this);
            AuthorsCollection = new CollectionAuthors(this);
            CategoriesCollection = new CollectionCategories(this);
            TagsCollection = new CollectionTags(this);

            RegisterCollections();
        }
        #endregion Constructor
    }
}
