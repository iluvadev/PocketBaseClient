using pocketbase_csharp_sdk.Json;
using System.Text.Json.Serialization;

namespace PocketBaseClient.Orm
{
    public abstract class ItemBase
    {
        private ItemMetadata? _Metadata = null;
        public ItemMetadata Metadata => _Metadata ??= new ItemMetadata(this);

        public CollectionBase? Collection { get; internal set; }

        //public string? CollectionId { get; set; }
        //public string? CollectionName { get; set; }

        public string? Id { get; init; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Created { get; set; }

        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Updated { get; set; }


        public bool IsValid()
            => Metadata.IsLoaded && !Metadata.IsTrash;

    }
}
