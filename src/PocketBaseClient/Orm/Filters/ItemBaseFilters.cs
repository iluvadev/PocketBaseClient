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
    public class ItemBaseFilters
    {
        /// <summary>Makes a Filter to Query data over the 'id' field</summary>
        public FieldFilterText Id => new FieldFilterText("id");

        /// <summary>Makes a Filter to Query data over the 'created' field</summary>
        public FieldFilterDate Created => new FieldFilterDate("created");

        /// <summary>Makes a Filter to Query data over the 'updated' field</summary>
        public FieldFilterDate Updated => new FieldFilterDate("updated");
    }
}
