// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase


using pocketbase_csharp_sdk.Models;
using pocketbase_csharp_sdk.Models.Log;
using pocketbase_csharp_sdk.Services;
using PocketBaseClient.Services;
using System.Web;

namespace PocketBaseClient.Orm
{
    /// <summary>
    /// Base class for an Authentication Collection of PocketBase, with registries mapped in the ORM
    /// </summary>
    /// <typeparam name="T">The type of the mapped registries</typeparam>
    public abstract partial class CollectionAuthBase<T> : CollectionBase<T>
        where T : ItemAuthBase, new()
    {
        private AuthCollectionService<T>? _Auth;
        public AuthCollectionService<T> Auth => _Auth ??= new AuthCollectionService<T>(this);

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context"></param>
        protected CollectionAuthBase(DataServiceBase context) : base(context) { }
    }
}
