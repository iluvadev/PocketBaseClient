
// This file was generated automatically on 8/12/2022 19:37:08(UTC) from the PocketBase schema for Application orm-csharp-test (https://orm-csharp-test.pockethost.io)
//
// PocketBaseClient-csharp project: https://github.com/iluvadev/PocketBaseClient-csharp
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using PocketBaseClient;
using PocketBaseClient.SampleApp.Services;

namespace PocketBaseClient.SampleApp
{
    public partial class OrmCsharpTestApplication : PocketBaseClientApplication
    {
        private OrmCsharpTestDataService? _Data = null;
        public OrmCsharpTestDataService Data => _Data ??= new OrmCsharpTestDataService(this);

        #region Constructors
        public OrmCsharpTestApplication() : this("https://orm-csharp-test.pockethost.io") { }
        public OrmCsharpTestApplication(string url, string appName = "orm-csharp-test") : base(url, appName) { }
        #endregion Constructors
    }
}
