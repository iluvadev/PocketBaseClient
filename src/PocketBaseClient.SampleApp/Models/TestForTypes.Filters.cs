
// This file was generated automatically on 8/12/2022 21:42:16(UTC) from the PocketBase schema for Application orm-csharp-test (https://orm-csharp-test.pockethost.io)
//
// PocketBaseClient-csharp project: https://github.com/iluvadev/PocketBaseClient-csharp
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using PocketBaseClient.Orm.Filters;
using System.Net.Mail;

namespace PocketBaseClient.SampleApp.Models
{
    public partial class TestForTypes 
    {
        public class Filters : ItemBaseFilters
        {

            public FilterQuery TextNoRestrictions(OperatorText op, string value) => FilterQuery.Create("text_no_restrictions", op, value);
            public FilterQuery TextRestrictions(OperatorText op, string value) => FilterQuery.Create("text_restrictions", op, value);
            public FilterQuery NumberNoRestrictions(OperatorNumeric op, int value) => FilterQuery.Create("number_no_restrictions", op, value);
            public FilterQuery NumberRestrrictions(OperatorNumeric op, int value) => FilterQuery.Create("number_restrrictions", op, value);
            public FilterQuery Bool(bool value) => FilterQuery.Create("bool", value);
            public FilterQuery EmailNoRestrictions(OperatorText op, MailAddress value) => FilterQuery.Create("email_no_restrictions", op, value);
            public FilterQuery EmailNoRestrictions(OperatorText op, string value) => FilterQuery.Create("email_no_restrictions", op, value);
            public FilterQuery EmailRestrictionsExcept(OperatorText op, MailAddress value) => FilterQuery.Create("email_restrictions_except", op, value);
            public FilterQuery EmailRestrictionsExcept(OperatorText op, string value) => FilterQuery.Create("email_restrictions_except", op, value);
            public FilterQuery EmailRestrictionsOnly(OperatorText op, MailAddress value) => FilterQuery.Create("email_restrictions_only", op, value);
            public FilterQuery EmailRestrictionsOnly(OperatorText op, string value) => FilterQuery.Create("email_restrictions_only", op, value);
            public FilterQuery UrlNoRestrictions(OperatorText op, Uri value) => FilterQuery.Create("url_no_restrictions", op, value);
            public FilterQuery UrlNoRestrictions(OperatorText op, string value) => FilterQuery.Create("url_no_restrictions", op, value);
            public FilterQuery UrlRestrictionsExcept(OperatorText op, Uri value) => FilterQuery.Create("url_restrictions_except", op, value);
            public FilterQuery UrlRestrictionsExcept(OperatorText op, string value) => FilterQuery.Create("url_restrictions_except", op, value);
            public FilterQuery UrlRestrictionsOnly(OperatorText op, Uri value) => FilterQuery.Create("url_restrictions_only", op, value);
            public FilterQuery UrlRestrictionsOnly(OperatorText op, string value) => FilterQuery.Create("url_restrictions_only", op, value);
            public FilterQuery DatetimeNoRestrictions(OperatorNumeric op, DateTime value) => FilterQuery.Create("datetime_no_restrictions", op, value);
            public FilterQuery DatetimeRestrictions(OperatorNumeric op, DateTime value) => FilterQuery.Create("datetime_restrictions", op, value);

        }
    }
}
