using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PocketBaseClient.CodeGenerator.Models
{
    public class PocketBaseApplicationModel
    {
        [JsonPropertyName("appName")]
        public string? AppName { get; set; }

        [JsonPropertyName("appUrl")]
        public string? AppUrl { get; set; }
    }
}
