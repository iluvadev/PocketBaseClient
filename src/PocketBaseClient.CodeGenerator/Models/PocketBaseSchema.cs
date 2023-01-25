// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using System.Text.Json;
using pocketbase_csharp_sdk.Models.Collection;

namespace PocketBaseClient.CodeGenerator.Models
{
    /// <summary>
    /// Model to map the PocketBase schema 
    /// </summary>
    public class PocketBaseSchema
    {
        #region Properties
        private string? _ProjectName = null;
        /// <summary>
        /// The generated Project name
        /// </summary>
        public string ProjectName
        {
            get => _ProjectName ?? ("PocketBaseClient." + (PocketBaseApplication.Name ?? "Application")).ToPascalCaseForNamespace();
            set => _ProjectName = value;
        }

        private string? _Namespace = null;
        /// <summary>
        /// The generated Namespace
        /// </summary>
        public string Namespace
        {
            get => _Namespace ?? ProjectName.ToNamespace();
            set => _Namespace = value;
        }

        /// <summary>
        /// PocketBase application information
        /// </summary>
        public PocketBaseApplicationModel PocketBaseApplication { get; set; } = new PocketBaseApplicationModel();

        /// <summary>
        /// The date where Schema was downloaded
        /// </summary>
        public DateTime SchemaDate { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// Option to Singularize and Pluralize generated code
        /// </summary>
        public bool SingularizeAndPluralize { get; set; } = true;

        /// <summary>
        /// List of Collections of the PocketBase application
        /// </summary>
        public List<CollectionModel> Collections { get; set; } = new List<CollectionModel>();
        #endregion Properties


        /// <summary>
        /// Updates the PocketBase application information with data downloaded from server
        /// </summary>
        /// <param name="settings"></param>
        public void SetPocketBaseApplicationAsync(IDictionary<string, object>? settings)
        {
            if (settings?.ContainsKey("meta") ?? false)
                PocketBaseApplication = JsonSerializer.Deserialize<PocketBaseApplicationModel>(settings["meta"]?.ToString() ?? "") ?? new PocketBaseApplicationModel();
        }

        /// <summary>
        /// Saves the schema to a file (async)
        /// </summary>
        /// <param name="path">Path where save the schema</param>
        /// <returns></returns>
        public async Task SaveToFileAsync(string path)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var jsonString = JsonSerializer.Serialize(this, options);
            await File.WriteAllTextAsync(path, jsonString);
        }

        /// <summary>
        /// Saves the schema to a file
        /// </summary>
        /// <param name="path">Path where save the schema</param>
        /// <returns></returns>
        public void SaveToFile(string path)
        {
            Task.Run(async () => await SaveToFileAsync(path)).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Loads the Schema from file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static PocketBaseSchema? LoadFromFile(string path)
        {
            return JsonSerializer.Deserialize<PocketBaseSchema>(File.ReadAllText(path));
        }
    }
}
