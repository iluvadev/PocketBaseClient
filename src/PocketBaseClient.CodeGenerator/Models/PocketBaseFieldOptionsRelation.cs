using System.Text.Json.Serialization;

namespace PocketBaseClient.CodeGenerator.Models
{
    public class PocketBaseFieldOptionsRelation
    {
        [JsonPropertyName("maxSelect")]
        public int? MaxSelect { get; set; }
        public bool IsSinglSelect => (MaxSelect ?? 1) == 1;

        [JsonPropertyName("cascadeDelete")]
        public bool? CascadeDelete { get; set; }

        [JsonPropertyName("collectionId")]
        public bool? CollectionId { get; set; }
    }
}
