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
        public class Sorts : ItemBaseSorts
        {
            /// <summary>Makes a SortCommand to Order by the 'url' field</summary>
            public SortCommand Url => new SortCommand("url");

            /// <summary>Makes a SortCommand to Order by the 'method' field</summary>
            public SortCommand Method => new SortCommand("method");

            /// <summary>Makes a SortCommand to Order by the 'status' field</summary>
            public SortCommand Status => new SortCommand("status");

            /// <summary>Makes a SortCommand to Order by the 'auth' field</summary>
            public SortCommand Auth => new SortCommand("auth");

            /// <summary>Makes a SortCommand to Order by the 'remoteIp' field</summary>
            public SortCommand RemoteIP => new SortCommand("remoteIp");

            /// <summary>Makes a SortCommand to Order by the 'userIp' field</summary>
            public SortCommand UserIP => new SortCommand("userIp");

            /// <summary>Makes a SortCommand to Order by the 'referer' field</summary>
            public SortCommand Referer => new SortCommand("referer");

            /// <summary>Makes a SortCommand to Order by the 'userAgent' field</summary>
            public SortCommand UserAgent => new SortCommand("userAgent");
        }
    }
}
