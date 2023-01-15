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

namespace PocketBaseClient.CodeGenerator.Generation
{

    /// <summary>
    /// Information about a Collection, for the code generation
    /// </summary>
    internal class CollectionInfo
    {
        /// <summary>
        /// The PocketBase Collection model
        /// </summary>
        public CollectionModel CollectionModel { get; }

        /// <summary>
        /// Id of the Collection
        /// </summary>
        public string Id => CollectionModel.Id!;

        /// <summary>
        /// Name of the Collection as a natural name, with spaces between words
        /// </summary>
        private string NaturalName => CollectionModel.Name!.Replace("_", " ");

        /// <summary>
        /// Name of the collection in the DataService class, in the generated code
        /// </summary>
        public string NameInDataService => NaturalName.Pluralize().ToPascalCase() + "Collection";

        /// <summary>
        /// Name of the Class that maps the Collection, in the generated code
        /// </summary>
        public string ClassName => "Collection" + NaturalName.Pluralize().ToPascalCase();

        /// <summary>
        /// Filename where save the Collection class, in the generated code
        /// </summary>
        public string FileName => ClassName + ".cs";

        /// <summary>
        /// Name of the Class that represents the items of the collection, in the generated code
        /// </summary>
        private string ItemsClassName => NaturalName.Singularize().ToPascalCase();

        /// <summary>
        /// Information about the Items of the Collection
        /// </summary>
        public ItemInfo ItemInfo { get; }

        /// <summary>
        /// Function to get all Collections from the PocketBase schema, 
        /// used to obtain information about related Collections
        /// </summary>
        public Func<List<CollectionInfo>> AllCollectionsGetter { get; }


        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="collectionModel"></param>
        /// <param name="allCollectionsGetter"></param>
        /// <exception cref="Exception"></exception>
        public CollectionInfo(CollectionModel collectionModel, Func<List<CollectionInfo>> allCollectionsGetter)
        {
            if (collectionModel.Name == null) throw new Exception("Collection name is empty");
            if (collectionModel.Schema == null) throw new Exception($"Schema is empty for collection {collectionModel.Name}");
            if (collectionModel.Id == null) throw new Exception($"Collection Id is empty for collection {collectionModel.Name}");

            CollectionModel = collectionModel;
            ItemInfo = new ItemInfo(this, ItemsClassName);
            AllCollectionsGetter = allCollectionsGetter;
        }

        /// <summary>
        /// Generates code for the Collection and its items
        /// </summary>
        /// <param name="settings">Generation code settings</param>
        /// <returns></returns>
        public List<GeneratedCodeFile> GenerateCode(Settings settings)
        {
            var generatedFiles = new List<GeneratedCodeFile>();

            generatedFiles.Add(GetCodeFileForCollection(settings));
            generatedFiles.AddRange(ItemInfo.GenerateCode(settings));

            return generatedFiles;
        }

        /// <summary>
        /// Generates the code for the Collection
        /// </summary>
        /// <param name="settings">Generation code settings</param>
        /// <returns></returns>
        private GeneratedCodeFile GetCodeFileForCollection(Settings settings)
        {
            var fileName = Path.Combine(settings.PathModels, FileName);
            var content = $@"{settings.CodeHeader}
using PocketBaseClient.Orm;
using PocketBaseClient.Orm.Filters;
using PocketBaseClient.Services;

namespace {settings.NamespaceModels}
{{
    public partial class {ClassName} : CollectionBase<{ItemInfo.ClassName}>
    {{
        /// <inheritdoc />
        public override string Id => ""{CollectionModel.Id}"";

        /// <inheritdoc />
        public override string Name => ""{CollectionModel.Name}"";

        /// <inheritdoc />
        public override bool System => {(CollectionModel.System ?? false).ToString().ToLower()};

        /// <summary> Contructor: The Collection '{CollectionModel.Name}' </summary>
        /// <param name=""context"">The DataService for the collection</param>
        internal {ClassName}(DataServiceBase context) : base(context) {{ }}

        /// <summary> Query data at PocketBase, defining a Filter over collection '{CollectionModel.Name}' </summary>
        public CollectionQuery<{ClassName}, {ItemInfo.SortsClassName}, {ItemInfo.ClassName}> Filter(Func<{ItemInfo.FiltersClassName}, FilterCommand> filter)
            => new CollectionQuery<{ClassName}, {ItemInfo.SortsClassName}, {ItemInfo.ClassName}>(this, filter(new {ItemInfo.FiltersClassName}()));

        /// <summary> Query all data at PocketBase, over collection '{CollectionModel.Name}' </summary>
        public CollectionQuery<{ClassName}, {ItemInfo.SortsClassName}, {ItemInfo.ClassName}> All()
            => new CollectionQuery<{ClassName}, {ItemInfo.SortsClassName}, {ItemInfo.ClassName}>(this, null);

    }}
}}
";
            return new GeneratedCodeFile(fileName, content);
        }
    }
}
