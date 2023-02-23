// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase


namespace PocketBaseClient.Orm.Filters
{
    /// <summary>
    /// Definition for Sort options to sort filtered data in PocketBase
    /// </summary>
    public class ItemAuthBaseSorts : ItemBaseSorts
    {
        /// <summary>Makes a SortCommand to Order by the 'email' field</summary>
        public SortCommand Email => new SortCommand("email");

        /// <summary>Makes a SortCommand to Order by the 'emailVisibility' field</summary>
        public SortCommand EmailVisibility => new SortCommand("emailVisibility");

        /// <summary>Makes a SortCommand to Order by the 'username' field</summary>
        public SortCommand Username => new SortCommand("username");

        /// <summary>Makes a SortCommand to Order by the 'verified' field</summary>
        public SortCommand Verified => new SortCommand("verified");
    }
}
