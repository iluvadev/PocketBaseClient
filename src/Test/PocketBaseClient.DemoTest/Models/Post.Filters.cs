
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
using System.Net.Mail;

namespace PocketBaseClient.DemoTest.Models
{
    public partial class Post 
    {
        public class Filters : ItemBaseFilters
        {

            /// <summary>Makes a Filter to Query data over the 'title' field</summary>
            public FieldFilterText Title => new FieldFilterText("title");
            /// <summary>Makes a Filter to Query data over the 'author' field</summary>
            public FieldFilterItem<Author> Author => new FieldFilterItem<Author>("author");
            /// <summary>Makes a Filter to Query data over the 'summary' field</summary>
            public FieldFilterText Summary => new FieldFilterText("summary");
            /// <summary>Makes a Filter to Query data over the 'content' field</summary>
            public FieldFilterText Content => new FieldFilterText("content");
            /// <summary>Makes a Filter to Query data over the 'published' field</summary>
            public FieldFilterDate Published => new FieldFilterDate("published");
            /// <summary>Makes a Filter to Query data over the 'status' field</summary>
            public FieldFilterEnum<StatusEnum> Status => new FieldFilterEnum<StatusEnum>("status");
            /// <summary>Makes a Filter to Query data over the 'categories' field</summary>
            public FieldFilterItemList<CategoriesList, Category> Categories => new FieldFilterItemList<CategoriesList, Category>("categories");
            /// <summary>Makes a Filter to Query data over the 'tags' field</summary>
            public FieldFilterItemList<TagsList, Tag> Tags => new FieldFilterItemList<TagsList, Tag>("tags");

        }
    }
}
