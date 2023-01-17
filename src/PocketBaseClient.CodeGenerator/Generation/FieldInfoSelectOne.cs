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
    /// Information about a Field of type Select with only One value of an Item in a Collection, for the code generation
    /// </summary>
    internal class FieldInfoSelectOne : FieldInfo
    {
        /// <summary>
        /// Options of the field defined in PocketBase
        /// </summary>
        protected PocketBaseFieldOptionsSelect Options { get; }

        /// <inheritdoc />
        public override string TypeName => EnumName;

        /// <summary>
        /// The name of the generated Enum
        /// </summary>
        public string EnumName => PropertyName + "Enum";

        /// <summary>
        /// The file name where to save the generated enum
        /// </summary>
        private string EnumFileName => ItemInfo.ClassName + "." + EnumName + ".cs";

        /// <inheritdoc />
        public override string FilterType => $"FieldFilterEnum<{EnumName}>";
        
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="schemaField"></param>
        /// <param name="options"></param>
        public FieldInfoSelectOne(ItemInfo itemInfo, SchemaFieldModel schemaField, PocketBaseFieldOptionsSelect options) : base(itemInfo, schemaField)
        {
            Options = options;
        }

        /// <inheritdoc />
        public override List<GeneratedCodeFile> GenerateCode(Settings settings)
        {
            var list = base.GenerateCode(settings);
            list.Add(GetCodeFileForEnum(settings));

            return list;
        }

        /// <summary>
        /// Generates the code for the enum
        /// </summary>
        /// <param name="settings">Generation code settings</param>
        /// <returns></returns>
        private GeneratedCodeFile GetCodeFileForEnum(Settings settings)
        {
            var fileName = Path.Combine(settings.PathModels, EnumFileName);
            StringBuilder sb = new();
            sb.Append($@"{settings.CodeHeader}
using System.ComponentModel;

namespace {settings.NamespaceModels}
{{
    public partial class {ItemInfo.ClassName}
    {{
        public enum {EnumName}
        {{
");
            string indent = "            ";
            foreach (var value in Options.Values ?? new List<string>())
            {
                sb.AppendLine(@$"{indent}[Description(""{value}"")]");
                sb.AppendLine(@$"{indent}{value.ToPascalCase()},");
                sb.AppendLine();
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

            list.Add($@"[JsonConverter(typeof(EnumConverter<{EnumName}>))]");

            return list;
        }
    }
}
