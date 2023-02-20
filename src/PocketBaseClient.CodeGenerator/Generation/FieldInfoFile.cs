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
using PocketBaseClient.CodeGenerator.Models;
using System.Text;
using System.Text.Json;

namespace PocketBaseClient.CodeGenerator.Generation
{
    /// <summary>
    /// Information about a Field of type File of an Item in a Collection, for the code generation
    /// </summary>
    internal abstract class FieldInfoFile : FieldInfo
    {
        /// <summary>
        /// Options of the field defined in PocketBase
        /// </summary>
        protected PocketBaseFieldOptionsFile Options { get; }

        /// <summary>
        /// The name of the generated Type for the File
        /// </summary>
        protected string TypeFileName => PropertyName + "File";

        /// <summary>
        /// The file name where to save the generated Type for the File
        /// </summary>
        private string TypeFileFileName => ItemInfo.ClassName + "." + TypeFileName + ".cs";

        /// <inheritdoc />
        public override bool PrivateSetter => true;

        /// <inheritdoc />
        public override bool IsTypeNullableInProperty => false;

        /// <inheritdoc />
        public override string InitialValueForProperty => $"new {TypeName}(this)";

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="schemaField"></param>
        /// <param name="options"></param>
        public FieldInfoFile(ItemInfo itemInfo, SchemaFieldModel schemaField, PocketBaseFieldOptionsFile options) : base(itemInfo, schemaField)
        {
            Options = options;
        }

        /// <inheritdoc />
        public override List<GeneratedCodeFile> GenerateCode(Settings settings)
        {
            var list = base.GenerateCode(settings);

            list.Add(GetCodeFileForFile(settings));

            return list;
        }

        /// <summary>
        /// Generates the code for the File class
        /// </summary>
        /// <param name="settings">Generation code settings</param>
        /// <returns></returns>
        private GeneratedCodeFile GetCodeFileForFile(Settings settings)
        {
            var fileName = Path.Combine(settings.PathModels, TypeFileFileName);
            StringBuilder sb = new();
            sb.Append($@"{settings.CodeHeader}
using PocketBaseClient.Orm;
using PocketBaseClient.Orm.Structures;

namespace {settings.NamespaceModels}
{{
    public partial class {ItemInfo.ClassName}
    {{
        public class {TypeFileName} : FieldFileBase
        {{
");
            if (Options.MaxSize != null)
                sb.Append($@"
            /// <inheritdoc />
            public override long? MaxSize => {Options.MaxSize};
");
            sb.Append($@"
            public {TypeFileName}() : base(""{SchemaField.Name}"", owner: null) {{ }}

            public {TypeFileName}({ItemInfo.ClassName}? {ItemInfo.VarName}) : base(""{SchemaField.Name}"", {ItemInfo.VarName}) {{ }}
");
            if(Options.HasThumbs)
            {
                sb.Append(@"
            #region Thumbs");
                foreach (var thumb in Options.Thumbs!)
                    sb.Append($@"
            private Thumbnail? _Thumb{thumb} = null;
            public Thumbnail Thumb{thumb} => _Thumb{thumb} ??= new Thumbnail(this, ""{thumb}"");
");
                sb.Append(@"
            #endregion Thumbs");
            }

            sb.Append($@"
        }}
    }}
}}
");
            return new GeneratedCodeFile(fileName, sb.ToString());
        }


        /// <inheritdoc />
        protected override List<string> GetLinesForPropertyDecorators()
        {
            var list = base.GetLinesForPropertyDecorators();

            if (Options.MimeTypes?.Any() ?? false)
                list.Add($@"[MimeTypes(""{Options.MimeTypesJoined}"", ErrorMessage = ""Only MIME Types accepted: '{Options.MimeTypesJoined}'"")]");

            return list;
        }


        /// <summary>
        /// Factory for a Field info of type file
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="schemaField"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static FieldInfoFile NewFieldInfoFile(ItemInfo itemInfo, SchemaFieldModel schemaField)
        {
            if (schemaField.Type != "file")
                throw new Exception($"Field type '{schemaField.Type}' not expected for field '{schemaField.Name}' (expecting 'file')");

            var options = JsonSerializer.Deserialize<PocketBaseFieldOptionsFile>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsFile();
            if (options.IsMultiple)
                return new FieldInfoFileMultiple(itemInfo, schemaField, options);
            else
                return new FieldInfoFileOne(itemInfo, schemaField, options);
        }
    }
}
