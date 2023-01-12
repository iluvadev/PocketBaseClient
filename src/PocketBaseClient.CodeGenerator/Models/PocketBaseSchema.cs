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
        private string? _ProjectName = null;
        public string ProjectName
        {
            get => _ProjectName ?? ("PocketBaseClient." + (PocketBaseApplication.Name ?? "Application")).ToPascalCaseForNamespace();
            set => _ProjectName = value;
        }

        private string? _Namespace = null;
        public string Namespace
        {
            get => _Namespace ?? ProjectName.ToNamespace();
            set => _Namespace = value;
        }

        public PocketBaseApplicationModel PocketBaseApplication { get; set; } = new PocketBaseApplicationModel();


        public DateTime SchemaDate { get; set; } = DateTime.UtcNow;
        
        public bool SingularizeAndPluralize { get; set; } = true;

        public List<CollectionModel> Collections { get; set; } = new List<CollectionModel>();




        public void SetSettingsAsync(IDictionary<string, object>? settings)
        {
            if (settings?.ContainsKey("meta") ?? false)
                PocketBaseApplication = JsonSerializer.Deserialize<PocketBaseApplicationModel>(settings["meta"]?.ToString() ?? "") ?? new PocketBaseApplicationModel();
        }
        public async Task SaveToFileAsync(string path)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(this, options);
            await File.WriteAllTextAsync(path, jsonString);
        }
        public void SaveToFile(string path)
            => SaveToFileAsync(path).Wait();

        public static PocketBaseSchema? LoadFromFile(string path)
        {
            return JsonSerializer.Deserialize<PocketBaseSchema>(File.ReadAllText(path));
        }
    }
}
