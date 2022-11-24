using pocketbase_csharp_sdk;
using PocketClient.Services;

namespace PocketClient
{
    public class PocketClientAppication
    {
        public string? AppName { get; set; }
        public string AppUrl { get; init; }

        private AuthService? _Auth = null;
        public AuthService Auth => _Auth ??= new AuthService(this);

        public PocketBase Sdk { get; }

        public PocketClientAppication(string url, string? appName = null)
        {
            AppUrl = url;
            AppName = appName;
            Sdk = new PocketBase(url);
        }

    }
}
