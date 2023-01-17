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
    /// <summary>
    /// Information about a Field of type Url of an Item in a Collection, for the code generation
    /// </summary>
    internal class FieldInfoUrl : FieldInfo
    {
        /// <summary>
        /// Options of the field defined in PocketBase
        /// </summary>
        private PocketBaseFieldOptionsEmailUrl Options { get; }

        /// <inheritdoc />
        public override string TypeName => "Uri";

        /// <inheritdoc />
        public override string FilterType => "FieldFilterUri";

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="schemaField"></param>
        public FieldInfoUrl(ItemInfo itemInfo, SchemaFieldModel schemaField) : base(itemInfo, schemaField)
        {
            Options = JsonSerializer.Deserialize<PocketBaseFieldOptionsEmailUrl>(JsonSerializer.Serialize(schemaField.Options)) ?? new PocketBaseFieldOptionsEmailUrl();
        }

        /// <inheritdoc />
        protected override List<string> GetLinesForPropertyDecorators()
        {
            var list = base.GetLinesForPropertyDecorators();

            if (Options.OnlyDomains?.Any() ?? false)
                list.Add($@"[OnlyDomains(""{Options.OnlyDomainsJoined}"", ErrorMessage = ""Only domains accepted: '{Options.OnlyDomainsJoined}'"")]");
            else if (Options.ExceptDomains?.Any() ?? false)
                list.Add($@"[ExceptDomains(""{Options.ExceptDomainsJoined}"", ErrorMessage = ""Except domains accepted: '{Options.ExceptDomainsJoined}'"")]");

            list.Add("[JsonConverter(typeof(UrlConverter))]");
            return list;
        }
    }
}
