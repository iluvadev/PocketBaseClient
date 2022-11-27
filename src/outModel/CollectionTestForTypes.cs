
// This file was generated automatically on 26/11/2022 18:38:33 from the PocketBase schema for Application orm-csharp-test (https://orm-csharp-test.pockethost.io)
//
// PocketBaseClient-csharp project: https://github.com/iluvadev/PocketBaseClient-csharp
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using PocketBaseClient.Orm;
using PocketBaseClient.Services;

namespace TestingModel
{
    public partial class CollectionTestForTypes : CollectionBase<TestForTypes>
    {
        public override string Id => "fyyuitlkl66dgyt";
        public override string Name => "test_for_types";
        public override bool System => False;

        public CollectionTestForTypes(DataServiceBase context) : base(context) { }
    }
}
