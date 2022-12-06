
// This file was generated automatically on 6/12/2022 16:12:34(UTC) from the PocketBase schema for Application orm-csharp-test (https://orm-csharp-test.pockethost.io)
//
// PocketBaseClient-csharp project: https://github.com/iluvadev/PocketBaseClient-csharp
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using System.ComponentModel;

namespace PocketBaseClient.SampleApp.Models
{
    public partial class Post
    {
        public enum StatusEnum
        {
            [Description("draft")]
            Draft,

            [Description("to review")]
            ToReview,

            [Description("reviewed")]
            Reviewed,

            [Description("to publish")]
            ToPublish,

            [Description("published")]
            Published,


        }
    }
}
