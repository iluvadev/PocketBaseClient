
// This file was generated automatically for the PocketBase Application demo-test (https://orm-csharp-test.pockethost.io)
//    See CodeGenerationSummary.txt for more details
//
// PocketBaseClient-csharp project: https://github.com/iluvadev/PocketBaseClient-csharp
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using PocketBaseClient.Orm.Filters;

namespace PocketBaseClient.DemoTest.Models
{
    public partial class Post
    {
        public class Sorts : ItemBaseSorts
        {

            /// <summary>Makes a SortCommand to Order by the 'title' field</summary>
            public SortCommand Title => new SortCommand("title");

            /// <summary>Makes a SortCommand to Order by the 'author' field</summary>
            public SortCommand Author => new SortCommand("author");

            /// <summary>Makes a SortCommand to Order by the 'summary' field</summary>
            public SortCommand Summary => new SortCommand("summary");

            /// <summary>Makes a SortCommand to Order by the 'content' field</summary>
            public SortCommand Content => new SortCommand("content");

            /// <summary>Makes a SortCommand to Order by the 'published' field</summary>
            public SortCommand Published => new SortCommand("published");

            /// <summary>Makes a SortCommand to Order by the 'status' field</summary>
            public SortCommand Status => new SortCommand("status");

            /// <summary>Makes a SortCommand to Order by the 'categories' field</summary>
            public SortCommand Categories => new SortCommand("categories");

            /// <summary>Makes a SortCommand to Order by the 'tags' field</summary>
            public SortCommand Tags => new SortCommand("tags");


        }
    }
}
