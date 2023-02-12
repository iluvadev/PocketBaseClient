﻿// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using pocketbase_csharp_sdk;
using PocketBaseClient.Orm.Structures;
using PocketBaseClient.Services;

namespace PocketBaseClient.Orm
{
    /// <summary>
    /// Base class for a Collection in PocketBase
    /// </summary>
    public abstract partial class CollectionBase : IBasicCollection
    {
        /// <summary>
        /// The PocketBase 'id' of the Collection
        /// </summary>
        public abstract string? Id { get; }

        /// <summary>
        /// The PocketBase 'name' of the collection
        /// </summary>
        public abstract string? Name { get; }

        /// <summary>
        /// The PocketBase flag 'system' of the collection
        /// </summary>
        public abstract bool System { get; }

        //public DateTime? Created { get; set; }
        //public DateTime? Updated { get; set; }
        //public string? ListRule { get; set; }
        //public string? ViewRule { get; set; }
        //public string? CreateRule { get; set; }
        //public string? UpdateRule { get; set; }
        //public string? DeleteRule { get; set; }
        //public IEnumerable<SchemaFieldModel>? Schema { get; set; }

        /// <summary>
        /// The Context for Data access
        /// </summary>
        protected internal DataServiceBase Context { get; }


        /// <summary>
        /// Access to PocketBase Application
        /// </summary>
        protected internal PocketBaseClientApplication App => Context.App;


        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context"></param>
        internal CollectionBase(DataServiceBase context)
        {
            Context = context;
        }
    }
}
