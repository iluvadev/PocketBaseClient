
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
            public FilterQuery TextNoRestrictions(OperatorText op, string value) => FilterQuery.Create("text_no_restrictions", op, value);
            /// <summary>Makes a Filter to Query data over the 'text_restrictions' field</summary>
            public FilterQuery TextRestrictions(OperatorText op, string value) => FilterQuery.Create("text_restrictions", op, value);
            /// <summary>Makes a Filter to Query data over the 'number_no_restrictions' field</summary>
            public FilterQuery NumberNoRestrictions(OperatorNumeric op, int value) => FilterQuery.Create("number_no_restrictions", op, value);
            /// <summary>Makes a Filter to Query data over the 'number_restrrictions' field</summary>
            public FilterQuery NumberRestrrictions(OperatorNumeric op, int value) => FilterQuery.Create("number_restrrictions", op, value);
            /// <summary>Makes a Filter to Query data over the 'bool' field</summary>
            public FilterQuery Bool(bool value) => FilterQuery.Create("bool", value);
            /// <summary>Makes a Filter to Query data over the 'email_no_restrictions' field</summary>
            public FilterQuery EmailNoRestrictions(OperatorText op, MailAddress value) => FilterQuery.Create("email_no_restrictions", op, value);
            /// <summary>Makes a Filter to Query data over the 'email_no_restrictions' field</summary>
            public FilterQuery EmailNoRestrictions(OperatorText op, string value) => FilterQuery.Create("email_no_restrictions", op, value);
            /// <summary>Makes a Filter to Query data over the 'email_restrictions_except' field</summary>
            public FilterQuery EmailRestrictionsExcept(OperatorText op, MailAddress value) => FilterQuery.Create("email_restrictions_except", op, value);
            /// <summary>Makes a Filter to Query data over the 'email_restrictions_except' field</summary>
            public FilterQuery EmailRestrictionsExcept(OperatorText op, string value) => FilterQuery.Create("email_restrictions_except", op, value);
            /// <summary>Makes a Filter to Query data over the 'email_restrictions_only' field</summary>
            public FilterQuery EmailRestrictionsOnly(OperatorText op, MailAddress value) => FilterQuery.Create("email_restrictions_only", op, value);
            /// <summary>Makes a Filter to Query data over the 'email_restrictions_only' field</summary>
            public FilterQuery EmailRestrictionsOnly(OperatorText op, string value) => FilterQuery.Create("email_restrictions_only", op, value);
            /// <summary>Makes a Filter to Query data over the 'url_no_restrictions' field</summary>
            public FilterQuery UrlNoRestrictions(OperatorText op, Uri value) => FilterQuery.Create("url_no_restrictions", op, value);
            /// <summary>Makes a Filter to Query data over the 'url_no_restrictions' field</summary>
            public FilterQuery UrlNoRestrictions(OperatorText op, string value) => FilterQuery.Create("url_no_restrictions", op, value);
            /// <summary>Makes a Filter to Query data over the 'url_restrictions_except' field</summary>
            public FilterQuery UrlRestrictionsExcept(OperatorText op, Uri value) => FilterQuery.Create("url_restrictions_except", op, value);
            /// <summary>Makes a Filter to Query data over the 'url_restrictions_except' field</summary>
            public FilterQuery UrlRestrictionsExcept(OperatorText op, string value) => FilterQuery.Create("url_restrictions_except", op, value);
            /// <summary>Makes a Filter to Query data over the 'url_restrictions_only' field</summary>
            public FilterQuery UrlRestrictionsOnly(OperatorText op, Uri value) => FilterQuery.Create("url_restrictions_only", op, value);
            /// <summary>Makes a Filter to Query data over the 'url_restrictions_only' field</summary>
            public FilterQuery UrlRestrictionsOnly(OperatorText op, string value) => FilterQuery.Create("url_restrictions_only", op, value);
            /// <summary>Makes a Filter to Query data over the 'datetime_no_restrictions' field</summary>
            public FilterQuery DatetimeNoRestrictions(OperatorNumeric op, DateTime value) => FilterQuery.Create("datetime_no_restrictions", op, value);
            /// <summary>Makes a Filter to Query data over the 'datetime_restrictions' field</summary>
            public FilterQuery DatetimeRestrictions(OperatorNumeric op, DateTime value) => FilterQuery.Create("datetime_restrictions", op, value);

        }
    }
}
