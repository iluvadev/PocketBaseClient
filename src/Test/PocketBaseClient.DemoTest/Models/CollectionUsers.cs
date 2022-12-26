
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
    public partial class CollectionUsers : CollectionBase<User>
    {
        /// <inheritdoc />
        public override string Id => "_pb_users_auth_";

        /// <inheritdoc />
        public override string Name => "users";

        /// <inheritdoc />
        public override bool System => false;

        public CollectionUsers(DataServiceBase context) : base(context) { }


        /// <summary> Query data at PocketBase, defining a Filter over collection 'users' </summary>
        public CollectionQuery<CollectionUsers, User> Filter(string filterString)
             => new CollectionQuery<CollectionUsers, User>(this, FilterQuery.Create(filterString));

        /// <summary> Query data at PocketBase, defining a Filter over collection 'users' </summary>
        public CollectionQuery<CollectionUsers, User> Filter(Func<User.Filters, FilterQuery> filter)
            => new CollectionQuery<CollectionUsers, User>(this, filter(new User.Filters()));

    }
}
