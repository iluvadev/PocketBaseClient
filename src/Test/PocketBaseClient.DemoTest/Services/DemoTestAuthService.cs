
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
using PocketBaseClient.Orm;
using PocketBaseClient.DemoTest.Models;

namespace PocketBaseClient.DemoTest.Services
{
    public partial class DemoTestAuthService : AuthServiceBase
    {
        #region Auth Collections
        /// <summary> Auth for Collection 'users' in PocketBase </summary>
        public AuthCollectionService<User> User => (DemoTestDataService.GetCollection<User>() as CollectionAuthBase<User>)!.Auth;
        /// <summary> Auth for Collection 'users2' in PocketBase </summary>
        public AuthCollectionService<Users2> Users2 => (DemoTestDataService.GetCollection<Users2>() as CollectionAuthBase<Users2>)!.Auth;
        #endregion Auth Collections

        #region Constructor
        public DemoTestAuthService(PocketBaseClientApplication app) : base(app) { }
        #endregion Constructor
    }
}
