// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using pocketbase_csharp_sdk.Models.Collection;
using System.Text.Json;

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
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(this, options);
            File.WriteAllText(path, jsonString);
        }

        public static PocketBaseSchema? LoadFromFile(string path)
        {
            return JsonSerializer.Deserialize<PocketBaseSchema>(File.ReadAllText(path));
        }
    }
}
