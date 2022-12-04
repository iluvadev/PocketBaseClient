
// This file was generated automatically on 4/12/2022 0:34:34(UTC) from the PocketBase schema for Application orm-csharp-test (https://orm-csharp-test.pockethost.io)
//
// PocketBaseClient-csharp project: https://github.com/iluvadev/PocketBaseClient-csharp
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using PocketBaseClient.Orm;

namespace PocketBaseClient.SampleApp.Models
{
    public partial class TestForTypes
    {
        public class RelationMultipleLimitList : LimitedList<TestForTypes>
        {
            public RelationMultipleLimitList() : base(5) { }
        }
    }
}