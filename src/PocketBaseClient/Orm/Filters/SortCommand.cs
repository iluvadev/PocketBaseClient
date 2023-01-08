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
    /// Encapsulates a Command to be executed in a Sort query in PocketBase
    /// </summary>
    public class SortCommand
    {
        private List<SortCommand> _InnerSorts = new List<SortCommand>();

        private string InnerCommand { get; }

        /// <summary>
        /// The Command to be executed
        /// </summary>
        public string Command
        {
            get
            {
                var strCommand = InnerCommand;
                foreach (var innerSort in _InnerSorts)
                    strCommand += "," + innerSort.Command;
                return strCommand;
            }
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="innerCommand"></param>
        public SortCommand(string innerCommand)
        {
            InnerCommand = innerCommand;
        }

        /// <summary>
        /// Sort mode: ASC
        /// </summary>
        /// <returns></returns>
        public SortCommand Asc() => this;

        /// <summary>
        /// Sort mode: DESC
        /// </summary>
        /// <returns></returns>
        public SortCommand Desc() => new SortCommand("-" + InnerCommand);

        /// <summary>
        /// Adds a sort commant to the sort
        /// </summary>
        /// <param name="filterCommand">The sort directive to add</param>
        /// <returns></returns>
        public SortCommand AndThenBy(SortCommand filterCommand)
        {
            _InnerSorts.Add(filterCommand);
            return this;
        }
    }
}
