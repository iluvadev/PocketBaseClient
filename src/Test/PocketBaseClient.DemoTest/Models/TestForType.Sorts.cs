
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
    public partial class TestForType
    {
        public class Sorts : ItemBaseSorts
        {

            /// <summary>Makes a SortCommand to Order by the 'text_no_restrictions' field</summary>
            public SortCommand TextNoRestrictions => new SortCommand("text_no_restrictions");

            /// <summary>Makes a SortCommand to Order by the 'text_restrictions' field</summary>
            public SortCommand TextRestrictions => new SortCommand("text_restrictions");

            /// <summary>Makes a SortCommand to Order by the 'number_no_restrictions' field</summary>
            public SortCommand NumberNoRestrictions => new SortCommand("number_no_restrictions");

            /// <summary>Makes a SortCommand to Order by the 'number_restrrictions' field</summary>
            public SortCommand NumberRestrrictions => new SortCommand("number_restrrictions");

            /// <summary>Makes a SortCommand to Order by the 'bool' field</summary>
            public SortCommand Bool => new SortCommand("bool");

            /// <summary>Makes a SortCommand to Order by the 'email_no_restrictions' field</summary>
            public SortCommand EmailNoRestrictions => new SortCommand("email_no_restrictions");

            /// <summary>Makes a SortCommand to Order by the 'email_restrictions_except' field</summary>
            public SortCommand EmailRestrictionsExcept => new SortCommand("email_restrictions_except");

            /// <summary>Makes a SortCommand to Order by the 'email_restrictions_only' field</summary>
            public SortCommand EmailRestrictionsOnly => new SortCommand("email_restrictions_only");

            /// <summary>Makes a SortCommand to Order by the 'url_no_restrictions' field</summary>
            public SortCommand UrlNoRestrictions => new SortCommand("url_no_restrictions");

            /// <summary>Makes a SortCommand to Order by the 'url_restrictions_except' field</summary>
            public SortCommand UrlRestrictionsExcept => new SortCommand("url_restrictions_except");

            /// <summary>Makes a SortCommand to Order by the 'url_restrictions_only' field</summary>
            public SortCommand UrlRestrictionsOnly => new SortCommand("url_restrictions_only");

            /// <summary>Makes a SortCommand to Order by the 'datetime_no_restrictions' field</summary>
            public SortCommand DatetimeNoRestrictions => new SortCommand("datetime_no_restrictions");

            /// <summary>Makes a SortCommand to Order by the 'datetime_restrictions' field</summary>
            public SortCommand DatetimeRestrictions => new SortCommand("datetime_restrictions");

            /// <summary>Makes a SortCommand to Order by the 'select_single' field</summary>
            public SortCommand SelectSingle => new SortCommand("select_single");

            /// <summary>Makes a SortCommand to Order by the 'select_multiple' field</summary>
            public SortCommand SelectMultiple => new SortCommand("select_multiple");

            /// <summary>Makes a SortCommand to Order by the 'json' field</summary>
            public SortCommand Json => new SortCommand("json");

            /// <summary>Makes a SortCommand to Order by the 'file_single_no_restriction' field</summary>
            public SortCommand FileSingleNoRestriction => new SortCommand("file_single_no_restriction");

            /// <summary>Makes a SortCommand to Order by the 'file_single_restriction' field</summary>
            public SortCommand FileSingleRestriction => new SortCommand("file_single_restriction");

            /// <summary>Makes a SortCommand to Order by the 'file_multiple_no_restrictions' field</summary>
            public SortCommand FileMultipleNoRestrictions => new SortCommand("file_multiple_no_restrictions");

            /// <summary>Makes a SortCommand to Order by the 'file_multiple_restrictions' field</summary>
            public SortCommand FileMultipleRestrictions => new SortCommand("file_multiple_restrictions");

            /// <summary>Makes a SortCommand to Order by the 'reation_single' field</summary>
            public SortCommand ReationSingle => new SortCommand("reation_single");

            /// <summary>Makes a SortCommand to Order by the 'relation_multiple_no_limit' field</summary>
            public SortCommand RelationMultipleNoLimit => new SortCommand("relation_multiple_no_limit");

            /// <summary>Makes a SortCommand to Order by the 'relation_multiple_limit' field</summary>
            public SortCommand RelationMultipleLimit => new SortCommand("relation_multiple_limit");


        }
    }
}
