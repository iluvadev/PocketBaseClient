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
    public class ItemBaseSorts
    {
        /// <summary>Makes a SortCommand to Order by the 'id' field</summary>
        public SortCommand Id => new SortCommand("id");

        /// <summary>Makes a SortCommand to Order by the 'created' field</summary>
        public SortCommand Created => new SortCommand("created");

        /// <summary>Makes a SortCommand to Order by the 'updated' field</summary>
        public SortCommand Updated => new SortCommand("updated");
    }
}
