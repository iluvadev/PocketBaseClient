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
    /// Encapsulates a Command to be executed in a Filter query in PocketBase
    /// </summary>
    public class FilterCommand
    {
        private List<FilterCommandComposed> _InnerFilters = new List<FilterCommandComposed>();

        private string InnerCommand { get; }

        /// <summary>
        /// The Command to be executed
        /// </summary>
        public string Command
        {
            get
            {
                var strCommand = InnerCommand;
                foreach (var innerFilter in _InnerFilters)
                    strCommand += innerFilter.Command;
                return strCommand;
            }
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="innerCommand"></param>
        public FilterCommand(string innerCommand)
        {
            InnerCommand = innerCommand;
        }

        /// <summary>
        /// Adds an AND operator to the filter
        /// </summary>
        /// <param name="filterCommand">The filter to be added under AND operator</param>
        /// <returns></returns>
        public FilterCommand And(FilterCommand filterCommand)
        {
            _InnerFilters.Add(new FilterCommandComposed(FilterCommandComposeOptions.And, filterCommand));
            return this;
        }

        /// <summary>
        /// Adds an OR operator to the filter
        /// </summary>
        /// <param name="filterCommand">The filter to be added under OR operator</param>
        /// <returns></returns>
        public FilterCommand Or(FilterCommand filterCommand)
        {
            _InnerFilters.Add(new FilterCommandComposed(FilterCommandComposeOptions.Or, filterCommand));
            return this;

        }
    }
}
