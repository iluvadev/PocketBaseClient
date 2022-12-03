
// This file was generated automatically on 3/12/2022 22:02:26(UTC) from the PocketBase schema for Application orm-csharp-test (https://orm-csharp-test.pockethost.io)
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
        public class SelectMultipleEnumList : LimitedList<SelectMultipleEnum>
        {
            public SelectMultipleEnumList() : base(10) { }
        }
    }
}
