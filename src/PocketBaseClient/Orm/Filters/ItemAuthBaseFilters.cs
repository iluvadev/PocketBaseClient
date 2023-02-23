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
    /// Definition for Filters to query data in PocketBase
    /// </summary>
    public class ItemAuthBaseFilters : ItemBaseFilters
    {
        /// <summary>Makes a Filter to Query data over the 'email' field</summary>
        public FieldFilterMailAddress Email => new FieldFilterMailAddress("email");

        /// <summary>Makes a Filter to Query data over the 'emailVisibility' field</summary>
        public FieldFilterBool EmailVisibility => new FieldFilterBool("emailVisibility");

        /// <summary>Makes a Filter to Query data over the 'username' field</summary>
        public FieldFilterText Username => new FieldFilterText("username");

        /// <summary>Makes a Filter to Query data over the 'verified' field</summary>
        public FieldFilterBool Verified => new FieldFilterBool("verified");
    }
}
