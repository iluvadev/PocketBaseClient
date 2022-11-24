using pocketbase_csharp_sdk.Models;
using pocketbase_csharp_sdk.Models.Auth;
using pocketbase_csharp_sdk.Models.Log;

namespace PocketBaseClient.Services
{
    public class AuthUserService: ServiceBase
    {
        public AuthUserService(PocketBaseClientApplication app): base(app) { }

        public async Task<UserModel> CreateAsync(string email, string password, string passwordConfirm)
            => await App.Sdk.User.CreateAsync(email, password, passwordConfirm);

        public async Task<AuthMethodsList?> GetAuthenticationMethodsAsync()
            => await App.Sdk.User.GetAuthenticationMethodsAsync();

        public async Task<UserAuthModel?> AuthenticateWithPassword(string email, string password)
            => await App.Sdk.User.AuthenticateWithPassword(email, password);

        public async Task<UserAuthModel?> AuthenticateViaOAuth2(string provider, string code, string codeVerifier, string redirectUrl)
            => await App.Sdk.User.AuthenticateViaOAuth2(provider, code, codeVerifier, redirectUrl);

        public async Task<UserAuthModel?> RefreshAsync()
            => await App.Sdk.User.RefreshAsync();

        public async Task RequestPasswordResetAsync(string email)
            => await App.Sdk.User.RequestPasswordResetAsync(email);

        public async Task<UserAuthModel?> ConfirmPasswordResetAsync(string passwordResetToken, string password, string passwordConfirm)
            => await App.Sdk.User.ConfirmPasswordResetAsync(passwordResetToken, password, passwordConfirm);

        public async Task RequestVerificationAsync(string email)
            => await App.Sdk.User.RequestVerificationAsync(email);

        public async Task<UserAuthModel?> ConfirmVerificationAsync(string token)
            => await App.Sdk.User.ConfirmVerificationAsync(token);

        public async Task RequestEmailChangeAsync(string newEmail)
            => await App.Sdk.User.RequestEmailChangeAsync(newEmail);

        public async Task<UserAuthModel?> ConfirmEmailChangeAsync(string emailChangeToken, string userPassword)
            => await App.Sdk.User.ConfirmEmailChangeAsync(emailChangeToken, userPassword);

        public async Task<IEnumerable<ExternalAuthModel>?> GetExternalAuthenticationMethods(string userId)
            => await App.Sdk.User.GetExternalAuthenticationMethods(userId);
    }
}
