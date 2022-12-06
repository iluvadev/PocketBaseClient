using pocketbase_csharp_sdk.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PocketBaseClient.Orm
{
    public class ItemMetadata
    {
        [JsonIgnore]
        public ItemBase Item { get; private init; }

        public bool IsNew => Item.Created == null || Item.Updated == null;
        public bool IsLoaded => !IsNew && LastLoad != null;

        public bool IsTrash { get; internal set; } = false;

        public bool IsFromCache => Item.Collection.CacheContains(Item);

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? LastLoad { get; private set; } = null;

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? FirstChange { get; private set; } = null;

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? LastChange { get; private set; } = null;

        private bool _HasLocalChanges = false;
        public bool HasLocalChanges
        {
            get => _HasLocalChanges;
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
        }
    }
}
