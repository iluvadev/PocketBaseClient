
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
        public class Filters : ItemBaseFilters
        {

            /// <summary> Gets a Filter to Query data over the 'title' field in PocketBase </summary>
            public FieldFilterText Title => new FieldFilterText("title");

            /// <summary> Gets a Filter to Query data over the 'author' field in PocketBase </summary>
            public FieldFilterItem<Author> Author => new FieldFilterItem<Author>("author");

            /// <summary> Gets a Filter to Query data over the 'summary' field in PocketBase </summary>
            public FieldFilterText Summary => new FieldFilterText("summary");

            /// <summary> Gets a Filter to Query data over the 'content' field in PocketBase </summary>
            public FieldFilterText Content => new FieldFilterText("content");

            /// <summary> Gets a Filter to Query data over the 'published' field in PocketBase </summary>
            public FieldFilterDate Published => new FieldFilterDate("published");

            /// <summary> Gets a Filter to Query data over the 'status' field in PocketBase </summary>
            public FieldFilterEnum<StatusEnum> Status => new FieldFilterEnum<StatusEnum>("status");

            /// <summary> Gets a Filter to Query data over the 'categories' field in PocketBase </summary>
            public FieldFilterItemList<CategoriesList, Category> Categories => new FieldFilterItemList<CategoriesList, Category>("categories");

            /// <summary> Gets a Filter to Query data over the 'tags' field in PocketBase </summary>
            public FieldFilterItemList<TagsList, Tag> Tags => new FieldFilterItemList<TagsList, Tag>("tags");


        }
    }
}
