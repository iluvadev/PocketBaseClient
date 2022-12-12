// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using System.Collections;

namespace PocketBaseClient.Orm
{
    internal interface ILimitableList : IEnumerable
    {
        int? MaxSize { get; }

        ItemBase? Owner { get; set; }

        void UpdateWith(ILimitableList? limitableList);
    }
}
