
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
    public partial class CollectionUsers2 : CollectionAuthBase<Users2>
    {
        /// <inheritdoc />
        public override string Id => "y89kc8kxl8vts1k";

        /// <inheritdoc />
        public override string Name => "users2";

        /// <inheritdoc />
        public override bool System => false;

        /// <summary> Contructor: The Collection 'users2' </summary>
        /// <param name="context">The DataService for the collection</param>
        internal CollectionUsers2(DataServiceBase context) : base(context) { }

        /// <summary> Query data at PocketBase, defining a Filter over collection 'users2' </summary>
        public CollectionQuery<CollectionUsers2, Users2.Sorts, Users2> Filter(Func<Users2.Filters, FilterCommand> filter)
            => new CollectionQuery<CollectionUsers2, Users2.Sorts, Users2>(this, filter(new Users2.Filters()));

        /// <summary> Query all data at PocketBase, over collection 'users2' </summary>
        public CollectionQuery<CollectionUsers2, Users2.Sorts, Users2> All()
            => new CollectionQuery<CollectionUsers2, Users2.Sorts, Users2>(this, null);

    }
}
