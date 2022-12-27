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
using System.Text.Json;
using System.Threading.Tasks;

namespace PocketBaseClient.CodeGenerator.Generation
{
    internal class FieldInfoNumber : FieldInfo
    {
        private PocketBaseFieldOptionsNumber Options { get; }
        public override string TypeName => "int?";
        public override string FilterType => "FieldFilterNumber";

        public FieldInfoNumber(ItemInfo itemInfo, SchemaFieldModel schemaField): base(itemInfo, schemaField) 
        {
            Options = JsonSerializer.Deserialize<PocketBaseFieldOptionsNumber>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsNumber();
        }

        protected override List<string> GetLinesForPropertyDecorators()
        {
            var list = base.GetLinesForPropertyDecorators();

            if (Options.Max != null || Options.Min != null)
            {
                var min = Options.Min ?? int.MinValue;
                var max = Options.Max ?? int.MaxValue;
                list.Add($@"[Range({min}, {max}, ErrorMessage = ""Minimum {min}, Maximum {max}"")]");
            }

            return list;
        }
    }
}
