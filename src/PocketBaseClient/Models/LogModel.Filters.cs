// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using PocketBaseClient.Orm.Filters;

namespace PocketBaseClient.Models
{
    public partial class LogModel
    {
        public class Filters : ItemBaseFilters
        {
            /// <summary> Gets a Filter to Query data over the 'url' field in PocketBase </summary>
            public FieldFilterText Url => new FieldFilterText("url");

            /// <summary> Gets a Filter to Query data over the 'method' field in PocketBase </summary>
            public FieldFilterText Method => new FieldFilterText("method");

            /// <summary> Gets a Filter to Query data over the 'status' field in PocketBase </summary>
            public FieldFilterNumberInt Status => new FieldFilterNumberInt("status");

            /// <summary> Gets a Filter to Query data over the 'auth' field in PocketBase </summary>
            public FieldFilterText Auth => new FieldFilterText("auth");

            /// <summary> Gets a Filter to Query data over the 'userIp' field in PocketBase </summary>
            public FieldFilterText UserIP => new FieldFilterText("userIp");

            /// <summary> Gets a Filter to Query data over the 'remoteIp' field in PocketBase </summary>
            public FieldFilterText RemoteIP => new FieldFilterText("remoteIp");

            /// <summary> Gets a Filter to Query data over the 'referer' field in PocketBase </summary>
            public FieldFilterText Referer => new FieldFilterText("referer");

            /// <summary> Gets a Filter to Query data over the 'userAgent' field in PocketBase </summary>
            public FieldFilterText UserAgent => new FieldFilterText("userAgent");
        }
    }
}
