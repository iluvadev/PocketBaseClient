
// This file was generated automatically on 8/12/2022 19:37:08(UTC) from the PocketBase schema for Application orm-csharp-test (https://orm-csharp-test.pockethost.io)
//
// PocketBaseClient-csharp project: https://github.com/iluvadev/PocketBaseClient-csharp
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using PocketBaseClient.Orm.Filters;
using System.Net.Mail;

namespace PocketBaseClient.SampleApp.Models
{
    public partial class TestForRelated 
    {
        public class Filters : ItemBaseFilters
        {

            public FilterQuery NumberUnique(OperatorNumeric op, int value) => FilterQuery.Create("number_unique", op, value);
            public FilterQuery NumberNonempty(OperatorNumeric op, int value) => FilterQuery.Create("number_nonempty", op, value);
            public FilterQuery NumberNonemptyUnique(OperatorNumeric op, int value) => FilterQuery.Create("number_nonempty_unique", op, value);

        }
    }
}
