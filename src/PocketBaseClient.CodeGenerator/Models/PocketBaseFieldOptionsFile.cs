using System.Text.Json.Serialization;

namespace PocketBaseClient.CodeGenerator.Models
{
    public class PocketBaseFieldOptionsFile
    {
        [JsonPropertyName("maxSelect")]
        public int? MaxSelect { get; set; }
        public bool IsSinglSelect => (MaxSelect ?? 1) == 1;

        [JsonPropertyName("maxSize")]
        public long? MaxSize { get; set; }

        [JsonPropertyName("mimeTypes")]
        public List<string>? MimeTypes { get; set; }

        [JsonPropertyName("thumbs")]
        public List<string>? Thumbs { get; set; }
    }
}
