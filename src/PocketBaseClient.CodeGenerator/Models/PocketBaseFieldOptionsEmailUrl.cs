using System.Text.Json.Serialization;

namespace PocketBaseClient.CodeGenerator.Models
{
    public class PocketBaseFieldOptionsEmailUrl
    {
        [JsonPropertyName("exceptDomains")]
        public List<string>? ExceptDomains { get; set; }
        public string ExceptDomainsJoined
        {
            get
            {
                if (!(ExceptDomains?.Any() ?? false)) return string.Empty;
                return string.Join(',', ExceptDomains);
            }
        }

        [JsonPropertyName("onlyDomains")]
        public List<string>? OnlyDomains { get; set; }

        public string OnlyDomainsJoined
        {
            get
            {
                if (!(OnlyDomains?.Any() ?? false)) return string.Empty;
                return string.Join(',', OnlyDomains);
            }
        }

    }
}
