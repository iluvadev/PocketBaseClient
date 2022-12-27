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
using System.Text.Json;

namespace PocketBaseClient.CodeGenerator.Generation
{
    internal class FieldInfoText : FieldInfo
    {
        private PocketBaseFieldOptionsText Options { get; }

        public override string TypeName => "string?";
        public override string FilterType => "FieldFilterText";


        public FieldInfoText(ItemInfo itemInfo, SchemaFieldModel schemaField) : base(itemInfo, schemaField)
        {
            Options = JsonSerializer.Deserialize<PocketBaseFieldOptionsText>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsText();
        }

        protected override List<string> GetLinesForPropertyDecorators()
        {
            var list = base.GetLinesForPropertyDecorators();

            var max = Options.Max;
            if (Options.Min != null) max ??= int.MaxValue;
            if (max != null)
            {
                if (Options.Min != null) list.Add($@"[StringLength({max}, MinimumLength = {Options.Min}, ErrorMessage = ""Minimum {Options.Min}, Maximum {max} characters"")]");
                else list.Add($@"[StringLength({max}, ErrorMessage = ""Maximum {max} characters"")]");
            }
            if (!string.IsNullOrEmpty(Options.Pattern))
                list.Add($@"[RegularExpression(@""{Options.Pattern}"", ErrorMessage = @""Pattern '{Options.Pattern}' not match"")]");

            return list;
        }
    }
}
