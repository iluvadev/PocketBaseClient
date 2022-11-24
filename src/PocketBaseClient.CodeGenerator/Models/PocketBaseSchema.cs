using pocketbase_csharp_sdk.Models.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PocketBaseClient.CodeGenerator.Models
{
    public class PocketBaseSchema
    {
        public List<CollectionModel> Collections { get; set; } = new List<CollectionModel>();
        public PocketBaseApplicationModel Application { get; set; } = new PocketBaseApplicationModel();

        public void SetSettingsAsync(IDictionary<string, object>? settings)
        {
            if (settings?.ContainsKey("meta") ?? false)
                Application = JsonSerializer.Deserialize<PocketBaseApplicationModel>(settings["meta"]?.ToString() ?? "") ?? new PocketBaseApplicationModel();
        }
        public void SaveToFile(string path)
        {
            var jsonString = JsonSerializer.Serialize(this);
            File.WriteAllText(path, jsonString);
        }

        public static PocketBaseSchema? LoadFromFile(string path)
        {
            return JsonSerializer.Deserialize<PocketBaseSchema>(File.ReadAllText(path));
        }
    }
}
