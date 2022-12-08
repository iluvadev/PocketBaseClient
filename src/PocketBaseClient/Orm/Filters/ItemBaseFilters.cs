using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketBaseClient.Orm.Filters
{
    public class ItemBaseFilters
    {
        public FilterQuery Id(OperatorText op, string operand) => FilterQuery.Create("id", op, operand);
        public FilterQuery Created(OperatorNumeric op, DateTime operand) => FilterQuery.Create("created", op, operand);
        public FilterQuery Updated(OperatorNumeric op, DateTime operand) => FilterQuery.Create("updated", op, operand);
    }
}
