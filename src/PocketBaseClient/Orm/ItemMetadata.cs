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
    public class ItemMetadata
    {
        [JsonIgnore]
        public ItemBase Item { get; private init; }

        private bool? _IsNew = null;
        public bool IsNew
        {
            get => _IsNew ?? Item.Created == null || Item.Updated == null;
            set => _IsNew = value;
        }
        public bool IsLoaded => !IsNew && LastLoad != null;

        public bool IsTrash { get; internal set; } = false;

        public bool IsCached => Item.Collection.CacheContains(Item);

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? LastLoad { get; private set; } = null;

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? FirstChange { get; private set; } = null;

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? LastChange { get; private set; } = null;

        private bool _HasLocalChanges = false;
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

        public bool IsValid => Item.IsValid();
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
