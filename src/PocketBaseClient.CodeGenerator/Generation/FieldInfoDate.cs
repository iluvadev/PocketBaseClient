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
using System.Text.Json;

namespace PocketBaseClient.CodeGenerator.Generation
{
    internal class FieldInfoDate : FieldInfo
    {
        private PocketBaseFieldOptionsDatetime Options { get; }
        public override string TypeName => "DateTime?";
        public override string FilterType => "FieldFilterDate";

        public FieldInfoDate(ItemInfo itemInfo, SchemaFieldModel schemaField) : base(itemInfo, schemaField)
        {
            Options = JsonSerializer.Deserialize<PocketBaseFieldOptionsDatetime>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsDatetime();
        }

        protected override List<string> GetLinesForPropertyDecorators()
        {
            var list = base.GetLinesForPropertyDecorators();

            if (Options.Max != null || Options.Min != null)
            {
                var min = Options.Min ?? DateTime.MinValue.ToUniversalTime();
                var max = Options.Max ?? DateTime.MaxValue.ToUniversalTime();
                list.Add($@"[Range(typeof(DateTime), ""{min}"", ""{max}"", ErrorMessage = ""Minimum '{min}', Maximum '{max}'"")]");
            }

            list.Add("[JsonConverter(typeof(DateTimeConverter))]");
            return list;
        }
    }
}
