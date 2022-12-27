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
    internal class FieldInfoUrl : FieldInfo
    {
        private PocketBaseFieldOptionsEmailUrl Options { get; }
        public override string TypeName => "Uri?";
        public override string FilterType => "FieldFilterUri";

        public FieldInfoUrl(ItemInfo itemInfo, SchemaFieldModel schemaField) : base(itemInfo, schemaField)
        {
            Options = JsonSerializer.Deserialize<PocketBaseFieldOptionsEmailUrl>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsEmailUrl();
        }

        protected override List<string> GetLinesForPropertyDecorators()
        {
            var list = base.GetLinesForPropertyDecorators();

            if (Options.OnlyDomains != null)
                list.Add($@"[OnlyDomains(""{Options.OnlyDomainsJoined}"", ErrorMessage = ""Only domains accepted: '{Options.OnlyDomainsJoined}'"")]");
            else if (Options.ExceptDomains != null)
                list.Add($@"[ExceptDomains(""{Options.ExceptDomainsJoined}"", ErrorMessage = ""Except domains accepted: '{Options.ExceptDomainsJoined}'"")]");

            list.Add("[JsonConverter(typeof(UrlConverter))]");
            return list;
        }
    }
}
