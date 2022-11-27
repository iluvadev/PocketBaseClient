using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PocketBaseClient.CodeGenerator.Models
{
    public class PocketBaseFieldOptionsNumber
    {
        [JsonPropertyName("max")]
        public int? Max { get; set; }

        [JsonPropertyName("min")]
        public int? Min { get; set; }
    }
}
