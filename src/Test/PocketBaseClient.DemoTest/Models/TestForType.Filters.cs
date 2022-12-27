
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
    public partial class TestForType 
    {
        public class Filters : ItemBaseFilters
        {

            /// <summary>Makes a Filter to Query data over the 'text_no_restrictions' field</summary>
            public FieldFilterText TextNoRestrictions => new FieldFilterText("text_no_restrictions");
            /// <summary>Makes a Filter to Query data over the 'text_restrictions' field</summary>
            public FieldFilterText TextRestrictions => new FieldFilterText("text_restrictions");
            /// <summary>Makes a Filter to Query data over the 'number_no_restrictions' field</summary>
            public FieldFilterNumber NumberNoRestrictions => new FieldFilterNumber("number_no_restrictions");
            /// <summary>Makes a Filter to Query data over the 'number_restrrictions' field</summary>
            public FieldFilterNumber NumberRestrrictions => new FieldFilterNumber("number_restrrictions");
            /// <summary>Makes a Filter to Query data over the 'bool' field</summary>
            public FieldFilterBool Bool => new FieldFilterBool("bool");
            /// <summary>Makes a Filter to Query data over the 'email_no_restrictions' field</summary>
            public FieldFilterMailAddress EmailNoRestrictions => new FieldFilterMailAddress("email_no_restrictions");
            /// <summary>Makes a Filter to Query data over the 'email_restrictions_except' field</summary>
            public FieldFilterMailAddress EmailRestrictionsExcept => new FieldFilterMailAddress("email_restrictions_except");
            /// <summary>Makes a Filter to Query data over the 'email_restrictions_only' field</summary>
            public FieldFilterMailAddress EmailRestrictionsOnly => new FieldFilterMailAddress("email_restrictions_only");
            /// <summary>Makes a Filter to Query data over the 'url_no_restrictions' field</summary>
            public FieldFilterUri UrlNoRestrictions => new FieldFilterUri("url_no_restrictions");
            /// <summary>Makes a Filter to Query data over the 'url_restrictions_except' field</summary>
            public FieldFilterUri UrlRestrictionsExcept => new FieldFilterUri("url_restrictions_except");
            /// <summary>Makes a Filter to Query data over the 'url_restrictions_only' field</summary>
            public FieldFilterUri UrlRestrictionsOnly => new FieldFilterUri("url_restrictions_only");
            /// <summary>Makes a Filter to Query data over the 'datetime_no_restrictions' field</summary>
            public FieldFilterDate DatetimeNoRestrictions => new FieldFilterDate("datetime_no_restrictions");
            /// <summary>Makes a Filter to Query data over the 'datetime_restrictions' field</summary>
            public FieldFilterDate DatetimeRestrictions => new FieldFilterDate("datetime_restrictions");
            /// <summary>Makes a Filter to Query data over the 'select_single' field</summary>
            public FieldFilterEnum<SelectSingleEnum> SelectSingle => new FieldFilterEnum<SelectSingleEnum>("select_single");
            /// <summary>Makes a Filter to Query data over the 'select_multiple' field</summary>
            public FieldFilterEnumList<SelectMultipleList, SelectMultipleEnum> SelectMultiple => new FieldFilterEnumList<SelectMultipleList, SelectMultipleEnum>("select_multiple");
            /// <summary>Makes a Filter to Query data over the 'reation_single' field</summary>
            public FieldFilterItem<TestForRelated> ReationSingle => new FieldFilterItem<TestForRelated>("reation_single");
            /// <summary>Makes a Filter to Query data over the 'relation_multiple_no_limit' field</summary>
            public FieldFilterItemList<RelationMultipleNoLimitList, TestForRelated> RelationMultipleNoLimit => new FieldFilterItemList<RelationMultipleNoLimitList, TestForRelated>("relation_multiple_no_limit");
            /// <summary>Makes a Filter to Query data over the 'relation_multiple_limit' field</summary>
            public FieldFilterItemList<RelationMultipleLimitList, TestForRelated> RelationMultipleLimit => new FieldFilterItemList<RelationMultipleLimitList, TestForRelated>("relation_multiple_limit");

        }
    }
}
