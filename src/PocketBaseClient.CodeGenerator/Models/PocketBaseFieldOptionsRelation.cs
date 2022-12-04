using System.Text.Json.Serialization;

namespace PocketBaseClient.CodeGenerator.Models
{
    public class PocketBaseFieldOptionsRelation
    {
        [JsonPropertyName("maxSelect")]
        public int? MaxSelect { get; set; }
        public bool IsSinglSelect => (MaxSelect ?? 0) == 1;

        [JsonPropertyName("cascadeDelete")]
        public bool? CascadeDelete { get; set; }

        [JsonPropertyName("collectionId")]
        public string? CollectionId { get; set; }
    }
}
