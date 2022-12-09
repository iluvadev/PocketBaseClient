// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using System.Net.Mail;

namespace PocketBaseClient.Orm.Filters
{
    public class FilterQuery
    {
        public string FilterString { get; }
        private FilterQuery(string filterString)
        {
            FilterString = filterString;
        }

        public override string ToString()
        {
            return FilterString;
        }

        #region Factory
        public static FilterQuery Create(string filterString)
            => new FilterQuery(filterString);
        public static FilterQuery Create(string operand1, OperatorText op, string value)
            => new FilterQuery($"{operand1}{op.OperatorString()}'{value}'");

        public static FilterQuery Create(string operand1, OperatorText op, MailAddress value)
            => new FilterQuery($"{operand1}{op.OperatorString()}'{value.Address}'");

        public static FilterQuery Create(string operand1, OperatorText op, Uri value)
            => new FilterQuery($"{operand1}{op.OperatorString()}'{value}'");

        public static FilterQuery Create<T>(string operand1, OperatorText op, T value) where T : struct, IConvertible
            => new FilterQuery($"{operand1}{op.OperatorString()}'{value.GetDescription() ?? value.ToString() ?? ""}'");

        public static FilterQuery Create(string operand1, OperatorNumeric op, int value)
            => new FilterQuery($"{operand1}{op.OperatorString()}{value}");

        public static FilterQuery Create(string operand1, bool value)
            => new FilterQuery($"{operand1}={value}");

        public static FilterQuery Create(string operand1, OperatorNumeric op, DateTime value)
            => new FilterQuery($"{operand1}{op.OperatorString()}'{value}'");

        #endregion Factory
    }
}
