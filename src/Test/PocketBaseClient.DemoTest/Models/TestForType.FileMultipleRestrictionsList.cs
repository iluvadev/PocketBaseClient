
// This file was generated automatically for the PocketBase Application demo-test (https://orm-csharp-test.pockethost.io)
//    See CodeGenerationSummary.txt for more details
//
// PocketBaseClient-csharp project: https://github.com/iluvadev/PocketBaseClient-csharp
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using PocketBaseClient.Orm.Structures;

namespace PocketBaseClient.DemoTest.Models
{
    public partial class TestForType
    {
        public class FileMultipleRestrictionsList : FieldFileList<FileMultipleRestrictionsFile>
        {
            public FileMultipleRestrictionsList() : this(null) { }

            public FileMultipleRestrictionsList(TestForType? testForType) : base(testForType, "FileMultipleRestrictions", "o4hs5o8n", 10) { }

            internal List<string> GetRemovedFileNames() => RemovedFileNames;
        }
    }
}
