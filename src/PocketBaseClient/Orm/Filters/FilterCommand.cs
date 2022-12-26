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
    public class FilterCommand
    {
        private List<ComposedFilterCommand> _InnerFilters = new List<ComposedFilterCommand>();

        private string InnerCommand { get; }

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
        public FilterCommand(string innerCommand)
        {
            InnerCommand = innerCommand;
        }


        public FilterCommand And(FilterCommand filterCommand)
        {
            _InnerFilters.Add(new ComposedFilterCommand(ComposeFilterOptions.And, filterCommand));
            return this;
        }

        public FilterCommand Or(FilterCommand filterCommand)
        {
            _InnerFilters.Add(new ComposedFilterCommand(ComposeFilterOptions.Or, filterCommand));
            return this;

        }
    }
}
