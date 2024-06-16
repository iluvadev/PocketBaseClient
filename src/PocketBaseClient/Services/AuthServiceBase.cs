// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using pocketbase_csharp_sdk;
using pocketbase_csharp_sdk.Services;

namespace PocketBaseClient.Services
{

    /// <summary>
    /// Authentication Operations 
    /// </summary>
    public class AuthServiceBase : ServiceBase
    {
        /// <summary>
        /// Current Authenticated information
        /// </summary>
        public AuthStore AuthStore => App.Sdk.AuthStore;

        private AuthAdminService? _Admin = null;
        /// <summary>
        /// Authenticate as Admin actions
        /// </summary>
        public AuthAdminService Admin => _Admin ??= new AuthAdminService(App);


        private UserService? _UserService;
        /// <summary>
        /// User Service
        /// </summary>
        public UserService UserService => _UserService ?? new UserService(App.Sdk);

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="app"></param>
        public AuthServiceBase(PocketBaseClientApplication app) : base(app) { }
    }
}
