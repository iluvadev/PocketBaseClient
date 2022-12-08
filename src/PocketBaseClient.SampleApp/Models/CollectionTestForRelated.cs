
// This file was generated automatically on 8/12/2022 0:36:23(UTC) from the PocketBase schema for Application orm-csharp-test (https://orm-csharp-test.pockethost.io)
//
// PocketBaseClient-csharp project: https://github.com/iluvadev/PocketBaseClient-csharp
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using PocketBaseClient.Orm;
using PocketBaseClient.Services;

namespace PocketBaseClient.SampleApp.Models
{
    public partial class CollectionTestForRelated : CollectionBase<TestForRelated>
    {
        public override string Id => "v2ge3yxdn90bhss";
        public override string Name => "test_for_related";
        public override bool System => false;

        public CollectionTestForRelated(DataServiceBase context) : base(context) { }
    }
}
