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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketBaseClient.CodeGenerator.Generation
{
    internal class FieldInfoJson : FieldInfo
    {
        public override string TypeName => "dynamic?";
        public override string FilterType => "FieldFilterText";

        public FieldInfoJson(ItemInfo itemInfo, SchemaFieldModel schemaFieldModel): base(itemInfo, schemaFieldModel) { }
    }
}
