
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
        public class FileMultipleNoRestrictionsList : FieldFileList<FileMultipleNoRestrictionsFile>
        {
            public FileMultipleNoRestrictionsList() : this(null) { }

            public FileMultipleNoRestrictionsList(TestForType? testForType) : base(testForType, "FileMultipleNoRestrictions", "mqokykua", 10) { }

            internal List<string> GetRemovedFileNames() => RemovedFileNames;

            internal List<string> GetAddedFileNames() => AddedFileNames;
        }
    }
}
