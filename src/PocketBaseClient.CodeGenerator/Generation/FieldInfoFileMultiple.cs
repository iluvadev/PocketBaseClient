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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PocketBaseClient.CodeGenerator.Generation
{

    // https://pocketbase.io/docs/files-handling/#deleting-files
    // https://stackoverflow.com/a/30401310/17372023
    //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    //[JsonConverter(typeof(InterfaceLabelConverter))]
    //public FileMultipleRestrictionsFile FileMultipleRestrictionsToRemove


    internal class FieldInfoFileMultiple : FieldInfoFile
    {
        /// <inheritdoc />
        public override string TypeName => ListClassName;

        /// <summary>
        /// The Class name of the type List if is multiple
        /// </summary>
        private string ListClassName => PropertyName + "List";

        /// <summary>
        /// The filename to the class for the list when is multiple
        /// </summary>
        private string ListFileName => ItemInfo.ClassName + "." + ListClassName + ".cs";

        /// <inheritdoc />
        public override string FilterType => "FieldFilterText";//$"FieldFilterItemList<{ListClassName}, {ReferencedClassName}>";


        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="schemaField"></param>
        /// <param name="options"></param>
        public FieldInfoFileMultiple(ItemInfo itemInfo, SchemaFieldModel schemaField, PocketBaseFieldOptionsFile options) : base(itemInfo, schemaField, options) { }

        /// <inheritdoc />
        public override List<GeneratedCodeFile> GenerateCode(Settings settings)
        {
            var list = base.GenerateCode(settings);

            list.Add(GetCodeFileForList(settings));

            return list;
        }

        /// <summary>
        /// Creates the code for the list of elements, when is multiple
        /// </summary>
        /// <param name="settings">Generation code settings</param>
        /// <returns></returns>
        private GeneratedCodeFile GetCodeFileForList(Settings settings)
        {
            var fileName = Path.Combine(settings.PathModels, ListFileName);
            var content = $@"{settings.CodeHeader}
using PocketBaseClient.Orm.Structures;

namespace {settings.NamespaceModels}
{{
    public partial class {ItemInfo.ClassName}
    {{
        public class {ListClassName} : FieldFileList<{TypeFileName}>
        {{
            public {ListClassName}() : this(null) {{ }}

            public {ListClassName}({ItemInfo.ClassName}? {ItemInfo.VarName}) : base({ItemInfo.VarName}, ""{PropertyName}"", ""{SchemaField.Id}"", {Options.MaxSelect?.ToString() ?? "null"}) {{ }}

            //internal List<string> GetRemovedFileNames() => RemovedFileNames;

            //internal List<string> GetAddedFileNames() => AddedFileNames;
        }}
    }}
}}
";
            return new GeneratedCodeFile(fileName, content);
        }

        /// <inheritdoc />
        protected override List<string> GetLinesForPropertyDecorators()
        {
            var list = base.GetLinesForPropertyDecorators();

            list.Add($@"[JsonConverter(typeof(FileListConverter<{ListClassName}, {TypeFileName}>))]");

            return list;
        }
    }
}
