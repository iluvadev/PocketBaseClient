using pocketbase_csharp_sdk.Json;
using System.Text.Json.Serialization;

namespace PocketBaseClient.Orm
{
    public class ItemMetadata
    {
        [JsonIgnore]
        public ItemBase Item { get; private init; }

        public bool IsCreated => Item.Id != null;
        public bool IsLoaded => IsCreated && Item.Updated != null;

        public bool IsTrash { get; internal set; } = false;

        public bool IsFromCache => Item.Collection.CacheContains(Item);

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? LastLoad { get; set; } = null;

        public bool HasLocalChanges { get; set; } = false;

        public ItemMetadata(ItemBase item)
        {
            Item = item;
        }

        public void SetLoaded()
        {
            LastLoad = DateTime.UtcNow;
            HasLocalChanges = false;
        }
    }
}
