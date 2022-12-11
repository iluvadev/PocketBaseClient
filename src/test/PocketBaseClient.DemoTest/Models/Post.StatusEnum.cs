
// This file was generated automatically for the PocketBase Application demo-test (https://orm-csharp-test.pockethost.io)
//    See CodeGenerationSummary.txt for more details
//
// PocketBaseClient-csharp project: https://github.com/iluvadev/PocketBaseClient-csharp
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using System.ComponentModel;

namespace PocketBaseClient.DemoTest.Models
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
