// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using pocketbase_csharp_sdk.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PocketBaseClient.Orm
{
    /// <summary>
    /// Metadata about an Item: an object mapped to a PocketBase Registry
    /// </summary>
    public class ItemMetadata
    {
        /// <summary> The item </summary>
        [JsonIgnore]
        internal ItemBase Item { get; private init; }

        private bool? _IsNew = null;

        /// <summary> The Item is created in memory and not saved to PocketBase?</summary>
        public bool IsNew
        {
            get => _IsNew ?? Item.Created == null || Item.Updated == null;
            internal set  => _IsNew = value;
        }

        /// <summary> The Item is loaded from PocketBase?</summary>
        public bool IsLoaded => !IsNew && LastLoad != null;

        /// <summary> The Item is marked as Trash, discarded?</summary>
        public bool IsTrash { get; internal set; } = false;

        /// <summary> The Item is in a Cache? </summary>
        public bool IsCached { get; set; } = false;

        /// <summary> The last time the Item was loaded from PocketBase </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? LastLoad { get; private set; } = null;

        /// <summary> The first time the Item was changed in memory and is not saved yet </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? FirstChange { get; private set; } = null;

        /// <summary> The last time the Item was changed in memory and is not saved yet </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? LastChange { get; private set; } = null;

        private bool _HasLocalChanges = false;
        /// <summary> The Item has changes in memory not saved yet? </summary>
        public bool HasLocalChanges
        {
            get => _HasLocalChanges || IsNew;
            internal set
            {
                if (value)
                {
                    var now = DateTime.UtcNow;
                    if (!_HasLocalChanges) FirstChange = now;
                    LastChange = now;
                }
                else
                    FirstChange = LastChange = null;
                _HasLocalChanges = value;
            }
        }
        /// <summary> The Item is Valid? (do not have validation errors) </summary>
        public bool IsValid => Item.IsValid();

        /// <summary> The List of validation errors for the Item </summary>
        public List<ValidationResult> ValidationErrors
        {
            get
            {
                Item.Validate(out var listErrors);
                return listErrors;
            }
        }

        internal ItemMetadata(ItemBase item)
        {
            Item = item;
        }

        internal void SetLoaded()
        {
            LastLoad = DateTime.UtcNow;
            HasLocalChanges = false;
            IsTrash = false;
        }

        internal void SetNeedBeLoaded()
        {
            LastLoad = null;
            HasLocalChanges = IsNew;
        }
    }
}
