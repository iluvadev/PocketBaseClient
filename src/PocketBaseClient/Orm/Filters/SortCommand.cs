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
    public class SortCommand
    {
        private List<SortCommand> _InnerSorts = new List<SortCommand>();

        private string InnerCommand { get; }

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

        public SortCommand(string innerCommand)
        {
            InnerCommand = innerCommand;
        }

        public SortCommand Asc() => this;
        public SortCommand Desc() => new SortCommand("-" + InnerCommand);
        public SortCommand AndThenBy(SortCommand filterCommand)
        {
            _InnerSorts.Add(filterCommand);
            return this;
        }
    }
}
