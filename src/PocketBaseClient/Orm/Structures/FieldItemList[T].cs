// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketBaseClient.Orm.Structures
{
    public class FieldItemList<T> : FieldBasicList<T>, IFieldItemList<T>
        where T : ItemBase, new()
    {

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyId"></param>
        /// <param name="maxSize"></param>
        public FieldItemList(ItemBase? owner, string propertyName, string propertyId, int? maxSize = null) 
            : base(owner, propertyName, propertyId, maxSize)
        {
        }

        public T? GetById(string? id, bool reload = false)
        {
            if (id == null) return null;
            var item = InnerList.FirstOrDefault(i=> i.Id == id);
            if (item == null) return null;
            
            if (reload) item.Reload();

            return item;
        }

        IEnumerable<T> IItemList<T>.GetItems(bool reload = false, GetItemsFilter include = GetItemsFilter.Load | GetItemsFilter.New)
        {
            foreach (var item in this)
            {
                if(reload)
                    item.Metadata_.SetNeedBeLoaded();

                // Check if item must be returned
                if (item.Metadata_.MatchFilter(include))
                    yield return item;
            }

        }

        public T? Remove(string? id)
            => Remove(GetById(id));

        public bool Delete(T? item)
            => Remove(item)?.Delete() ?? false;

        public bool Delete(string? id)
            => Delete(GetById(id));

    }
}
