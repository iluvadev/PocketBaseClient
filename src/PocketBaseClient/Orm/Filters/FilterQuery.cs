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
    /// <summary>
    /// Class to make a Filter in PocketBase
    /// </summary>
    public class FilterQuery
    {
        /// <summary>
        /// The string to send to Filter in PocketBase
        /// </summary>
        internal string FilterString { get; }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="filterString"></param>
        private FilterQuery(string filterString)
        {
            FilterString = filterString;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return FilterString;
        }

        #region Factory
        /// <summary>
        /// Creates a <see cref="FilterQuery"/> to filter data in PocketBase
        /// </summary>
        /// <param name="filterString"></param>
        /// <returns></returns>
        public static FilterQuery Create(string filterString)
            => new FilterQuery(filterString);

        /// <summary>
        /// Creates a <see cref="FilterQuery"/> to filter data in PocketBase
        /// </summary>
        /// <param name="operand1"></param>
        /// <param name="op"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FilterQuery Create(string operand1, OperatorText op, string value)
            => new FilterQuery($"{operand1}{op.OperatorString()}'{value}'");

        /// <summary>
        /// Creates a <see cref="FilterQuery"/> to filter data in PocketBase
        /// </summary>
        /// <param name="operand1"></param>
        /// <param name="op"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FilterQuery Create(string operand1, OperatorText op, MailAddress value)
            => new FilterQuery($"{operand1}{op.OperatorString()}'{value.Address}'");

        /// <summary>
        /// Creates a <see cref="FilterQuery"/> to filter data in PocketBase
        /// </summary>
        /// <param name="operand1"></param>
        /// <param name="op"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FilterQuery Create(string operand1, OperatorText op, Uri value)
            => new FilterQuery($"{operand1}{op.OperatorString()}'{value}'");

        /// <summary>
        /// Creates a <see cref="FilterQuery"/> to filter data in PocketBase
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="operand1"></param>
        /// <param name="op"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FilterQuery Create<T>(string operand1, OperatorText op, T value) where T : struct, IConvertible
            => new FilterQuery($"{operand1}{op.OperatorString()}'{value.GetDescription() ?? value.ToString() ?? ""}'");

        /// <summary>
        /// Creates a <see cref="FilterQuery"/> to filter data in PocketBase
        /// </summary>
        /// <param name="operand1"></param>
        /// <param name="op"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FilterQuery Create(string operand1, OperatorNumeric op, int value)
            => new FilterQuery($"{operand1}{op.OperatorString()}{value}");

        /// <summary>
        /// Creates a <see cref="FilterQuery"/> to filter data in PocketBase
        /// </summary>
        /// <param name="operand1"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FilterQuery Create(string operand1, bool value)
            => new FilterQuery($"{operand1}={value}");

        /// <summary>
        /// Creates a <see cref="FilterQuery"/> to filter data in PocketBase
        /// </summary>
        /// <param name="operand1"></param>
        /// <param name="op"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static FilterQuery Create(string operand1, OperatorNumeric op, DateTime value)
            => new FilterQuery($"{operand1}{op.OperatorString()}'{value}'");

        #endregion Factory
    }
}
