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
    internal class FilterCommandComposed
    {
        public FilterCommandComposeOptions ComposeFilterOption { get; }
        public FilterCommand FilterCommand { get; }

        public string Command
        {
            get
            {
                var strOperand = (ComposeFilterOption == FilterCommandComposeOptions.And) ? "&&" : "||";
                return $" {strOperand}({FilterCommand.Command})";
            }
        }

        public FilterCommandComposed(FilterCommandComposeOptions composeFilterOption, FilterCommand filterCommand)
        {
            ComposeFilterOption = composeFilterOption;
            FilterCommand = filterCommand;
        }
    }
}
