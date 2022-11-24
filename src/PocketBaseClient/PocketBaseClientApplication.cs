using pocketbase_csharp_sdk;
using PocketBaseClient.Services;

namespace PocketBaseClient
{
    public class PocketBaseClientApplication
    {
        public string? AppName { get; set; }
        public string AppUrl { get; init; }

        private AuthService? _Auth = null;
        public AuthService Auth => _Auth ??= new AuthService(this);

        public PocketBase Sdk { get; }

        public PocketBaseClientApplication(string url, string? appName = null)
        {
            AppUrl = url;
            AppName = appName;
            Sdk = new PocketBase(url);
        }

    }
}
