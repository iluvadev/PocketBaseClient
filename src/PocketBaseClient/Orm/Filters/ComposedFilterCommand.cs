// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketBaseClient.Orm.Filters
{
    internal class ComposedFilterCommand
    {
        public ComposeFilterOptions ComposeFilterOption { get; }
        public FilterCommand FilterCommand { get; }

        public string Command
        {
            get
            {
                var strOperand = (ComposeFilterOption == ComposeFilterOptions.And) ? "&&" : "||";
                return $" {strOperand}({FilterCommand.Command})";
            }
        }

        public ComposedFilterCommand(ComposeFilterOptions composeFilterOption, FilterCommand filterCommand)
        {
            ComposeFilterOption = composeFilterOption;
            FilterCommand = filterCommand;
        }
    }
}
