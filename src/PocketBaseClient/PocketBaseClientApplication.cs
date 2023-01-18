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
using PocketBaseClient.Services;

namespace PocketBaseClient
{
    /// <summary>
    /// Class for a Client of a concrete PocketBase application
    /// </summary>
    public partial class PocketBaseClientApplication
    {
        /// <summary>
        /// The Application Name
        /// </summary>
        public string? AppName { get; set; }

        /// <summary>
        /// The Url to connect with the application
        /// </summary>
        public string AppUrl { get; init; }

        private AuthService? _Auth = null;
        /// <summary>
        /// Authentication operations
        /// </summary>
        public AuthService Auth => _Auth ??= new AuthService(this);

        /// <summary>
        /// Access to the PocketBase Sdk
        /// </summary>
        public PocketBase Sdk { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="url"></param>
        /// <param name="appName"></param>
        public PocketBaseClientApplication(string url, string? appName = null)
        {
            AppUrl = url;
            AppName = appName;
            Sdk = new PocketBase(url);
        }

    }
}
