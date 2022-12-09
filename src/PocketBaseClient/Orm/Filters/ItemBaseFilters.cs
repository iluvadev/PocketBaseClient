// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

namespace PocketBaseClient.Orm.Filters
{
    public class ItemBaseFilters
    {
        public FilterQuery Id(OperatorText op, string operand) => FilterQuery.Create("id", op, operand);
        public FilterQuery Created(OperatorNumeric op, DateTime operand) => FilterQuery.Create("created", op, operand);
        public FilterQuery Updated(OperatorNumeric op, DateTime operand) => FilterQuery.Create("updated", op, operand);
    }
}
