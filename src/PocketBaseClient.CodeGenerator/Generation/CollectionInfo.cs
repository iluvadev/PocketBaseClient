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
    internal class CollectionInfo
    {
        public CollectionModel CollectionModel { get; }
        public string Id => CollectionModel.Id!;

        private string NaturalName => CollectionModel.Name!.Replace("_", " ");

        public string NameInDataService => NaturalName.Pluralize().ToPascalCase() + "Collection";
        public string ClassName => "Collection" + NaturalName.Pluralize().ToPascalCase();
        public string FileName => ClassName + ".cs";

        private string ItemsClassName => NaturalName.Singularize().ToPascalCase();

        public ItemInfo ItemInfo { get; }

        public Func<List<CollectionInfo>> AllCollectionsGetter { get; }


        public CollectionInfo(CollectionModel collectionModel, Func<List<CollectionInfo>> allCollectionsGetter)
        {
            if (collectionModel.Name == null) throw new Exception("Collection name is empty");
            if (collectionModel.Schema == null) throw new Exception($"Schema is empty for collection {collectionModel.Name}");
            if (collectionModel.Id == null) throw new Exception($"Collection Id is empty for collection {collectionModel.Name}");

            CollectionModel = collectionModel;
            ItemInfo = new ItemInfo(this, ItemsClassName);
            AllCollectionsGetter = allCollectionsGetter;
        }

        public List<GeneratedCodeFile> GenerateCode(Settings settings)
        {
            var generatedFiles = new List<GeneratedCodeFile>();

            generatedFiles.Add(GetCodeFileForCollection(settings));
            generatedFiles.AddRange(ItemInfo.GenerateCode(settings));

            return generatedFiles;
        }

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
