
// This file was generated automatically for the PocketBase Application demo-test (https://orm-csharp-test.pockethost.io)
//    See CodeGenerationSummary.txt for more details
//
// PocketBaseClient-csharp project: https://github.com/iluvadev/PocketBaseClient-csharp
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using PocketBaseClient;
using PocketBaseClient.DemoTest.Services;

namespace PocketBaseClient.DemoTest
{
    public partial class DemoTestApplication : PocketBaseClientApplication
    {
        private DemoTestDataService? _Data = null;
        /// <summary> Access to Data for Application demo-test </summary>
        public DemoTestDataService Data => _Data ??= new DemoTestDataService(this);

        private DemoTestAuthService? _Auth = null;
        /// <summary> Access to Auth for Application demo-test </summary>
        public new DemoTestAuthService Auth => _Auth ??= new DemoTestAuthService(this);

        #region Constructors
        public DemoTestApplication() : this("https://orm-csharp-test.pockethost.io") { }
        public DemoTestApplication(string url, string appName = "demo-test") : base(url, appName) { }
        #endregion Constructors
    }
}
