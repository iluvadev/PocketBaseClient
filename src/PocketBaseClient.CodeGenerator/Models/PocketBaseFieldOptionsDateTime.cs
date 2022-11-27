using PocketBaseClient.Orm.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PocketBaseClient.CodeGenerator.Models
{
    public class PocketBaseFieldOptionsDatetime
    {
        [JsonPropertyName("max")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Max { get; set; }

        [JsonPropertyName("min")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? Min { get; set; }
    }
}
