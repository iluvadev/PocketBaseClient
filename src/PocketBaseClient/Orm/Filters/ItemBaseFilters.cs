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
    /// <summary>
    /// Definition for Filters to query data in PocketBase
    /// </summary>
    public class ItemBaseFilters
    {
        /// <summary>
        /// Makes a Filter to Query data over the 'id' field
        /// </summary>
        /// <param name="op"></param>
        /// <param name="operand"></param>
        /// <returns></returns>
        public FilterQuery Id(OperatorText op, string operand) => FilterQuery.Create("id", op, operand);

        /// <summary>
        /// Makes a Filter to Query data over the 'created' field
        /// </summary>
        /// <param name="op"></param>
        /// <param name="operand"></param>
        /// <returns></returns>
        public FilterQuery Created(OperatorNumeric op, DateTime operand) => FilterQuery.Create("created", op, operand);

        /// <summary>
        /// Makes a Filter to Query data over the 'updated' field
        /// </summary>
        /// <param name="op"></param>
        /// <param name="operand"></param>
        /// <returns></returns>
        public FilterQuery Updated(OperatorNumeric op, DateTime operand) => FilterQuery.Create("updated", op, operand);
    }
}
