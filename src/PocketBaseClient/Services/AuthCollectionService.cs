using pocketbase_csharp_sdk.Models;
using pocketbase_csharp_sdk.Models.Auth;
using pocketbase_csharp_sdk.Models.Log;
using pocketbase_csharp_sdk.Services;
using PocketBaseClient.Orm;
using System.Web;

namespace PocketBaseClient.Services
{
    public class AuthCollectionService<T>
        where T : ItemAuthBase, new()
    {
        internal CollectionBase<T> Collection { get; private set; }

        private CollectionAuthService<RecordAuthModel<T>, T>? _AuthService;
        private CollectionAuthService<RecordAuthModel<T>, T> AuthService => _AuthService ??= new CollectionAuthService<RecordAuthModel<T>, T>(Collection.App.Sdk, Collection.Name!);

        public AuthCollectionService(CollectionBase<T> collection)
        {
            Collection = collection;
        }

        /// <summary>
        /// Create an Auth item with email and password (async)
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="passwordConfirm"></param>
        /// <returns></returns>
        public async Task<T?> CreateAsync(string email, string password, string passwordConfirm)
        {
            Dictionary<string, object> body = new()
            {
                { "email", email },
                { "password", password },
                { "passwordConfirm", passwordConfirm },
            };
            return await Collection.App.Sdk.HttpPostAsync<T>(Collection.UrlRecords, body);
        }
        /// <summary>
        /// Create an Auth item with email and password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="passwordConfirm"></param>
        /// <returns></returns>
        public T? Create(string email, string password, string passwordConfirm)
        {
            Dictionary<string, object> body = new()
            {
                { "email", email },
                { "password", password },
                { "passwordConfirm", passwordConfirm },
            };
            return Collection.App.Sdk.HttpPost<T>(Collection.UrlCollection, body);
        }

        /// <summary>
        /// Returns all available application authentication methods (async)
        /// </summary>
        /// <returns></returns>
        public Task<AuthMethodsList?> GetAuthenticationMethodsAsync()
             => AuthService.GetAuthenticationMethodsAsync();

        /// <summary>
        /// Returns all available application authentication methods
        /// </summary>
        /// <returns></returns>
        public AuthMethodsList? GetAuthenticationMethods()
            => AuthService.GetAuthenticationMethods();

        /// <summary>
        /// Authenticate with OAuth2 provider (async)
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="code"></param>
        /// <param name="codeVerifier"></param>
        /// <param name="redirectUrl"></param>
        /// <returns></returns>
        public async Task<T?> AuthenticateViaOAuth2Async(string provider, string code, string codeVerifier, string redirectUrl)
            => (await AuthService.AuthenticateViaOAuth2Async(provider, code, codeVerifier, redirectUrl))?.Record;

        /// <summary>
        /// Authenticate with OAuth2 provider
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="code"></param>
        /// <param name="codeVerifier"></param>
        /// <param name="redirectUrl"></param>
        /// <returns></returns>
        public T? AuthenticateViaOAuth2(string provider, string code, string codeVerifier, string redirectUrl)
            => AuthService.AuthenticateViaOAuth2(provider, code, codeVerifier, redirectUrl)?.Record;

        public async Task RequestVerificationAsync(string email)
            => await AuthService.RequestVerificationAsync(email);

        public void RequestVerification(string email)
            => AuthService.RequestVerification(email);

        public  Task ConfirmVerificationAsync(string token)  => AuthService.ConfirmVerificationAsync(token);

        public void ConfirmVerification(string token) => AuthService.ConfirmVerification(token);

        public async Task RequestEmailChangeAsync(string newEmail)
            => await AuthService.RequestEmailChangeAsync(newEmail);

        public void RequestEmailChange(string newEmail)
            => AuthService.RequestEmailChange(newEmail);

        public async Task ConfirmEmailChangeAsync(string emailChangeToken, string userPassword)
            => await AuthService.ConfirmEmailChangeAsync(emailChangeToken, userPassword);
        public void ConfirmEmailChange(string emailChangeToken, string userPassword)
            => AuthService.ConfirmEmailChange(emailChangeToken, userPassword);


        public async Task<IEnumerable<ExternalAuthModel>?> GetExternalAuthenticationMethodsAsync(string userId)
            => await AuthService.GetExternalAuthenticationMethodsAsync(userId);

        public IEnumerable<ExternalAuthModel>? GetExternalAuthenticationMethods(string userId)
            => AuthService.GetExternalAuthenticationMethods(userId);

        public async Task UnlinkExternalAuthenticationAsync(string userId, string provider)
            => await AuthService.UnlinkExternalAuthenticationAsync(userId, provider);

        public void UnlinkExternalAuthentication(string userId, string provider)
            => AuthService.UnlinkExternalAuthentication(userId, provider);


        public Task RequestPasswordResetAsync(string email) => AuthService.RequestPasswordResetAsync(email);
        public void RequestPasswordReset(string email) => AuthService.RequestPasswordReset(email);

        public Task ConfirmPasswordResetAsync(string passwordResetToken, string password, string passwordConfirm) => AuthService.ConfirmPasswordResetAsync(passwordResetToken, password, passwordConfirm);
        public void ConfirmPasswordReset(string passwordResetToken, string password, string passwordConfirm) => AuthService.ConfirmPasswordReset(passwordResetToken, password, passwordConfirm);
    }
}
