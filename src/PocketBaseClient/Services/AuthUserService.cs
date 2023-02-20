// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using pocketbase_csharp_sdk.Models;
using pocketbase_csharp_sdk.Models.Auth;
using pocketbase_csharp_sdk.Models.Log;

namespace PocketBaseClient.Services
{
    /// <summary>
    /// Encapsulates Authentication as User operations
    /// </summary>
    public class AuthUserService: ServiceBase
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="app"></param>
        public AuthUserService(PocketBaseClientApplication app): base(app) { }

        /// <summary>
        /// Create an User with email and password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="passwordConfirm"></param>
        /// <returns></returns>
        public async Task<UserModel> CreateAsync(string email, string password, string passwordConfirm)
            => await App.Sdk.User.CreateAsync(email, password, passwordConfirm);

        /// <summary>
        /// Gets available Authentication Methods
        /// </summary>
        /// <returns></returns>
        public async Task<AuthMethodsList?> GetAuthenticationMethodsAsync()
            => await App.Sdk.User.GetAuthenticationMethodsAsync();

        /// <summary>
        /// Authenticate an User with identifier and password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<UserAuthModel?> AuthenticateWithPasswordAsync(string email, string password)
            => await App.Sdk.User.AuthenticateWithPasswordAsync(email, password);

        /// <summary>
        /// Authenticate an User with OAuth2 provider
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="code"></param>
        /// <param name="codeVerifier"></param>
        /// <param name="redirectUrl"></param>
        /// <returns></returns>
        public async Task<UserAuthModel?> AuthenticateViaOAuth2Async(string provider, string code, string codeVerifier, string redirectUrl)
            => await App.Sdk.User.AuthenticateViaOAuth2Async(provider, code, codeVerifier, redirectUrl);

        /// <summary>
        /// Refresh User Authentication information
        /// </summary>
        /// <returns></returns>
        public async Task<UserAuthModel?> RefreshAsync()
            => await App.Sdk.User.RefreshAsync();

        /// <summary>
        /// Request a Password reset for an User
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task RequestPasswordResetAsync(string email)
            => await App.Sdk.User.RequestPasswordResetAsync(email);

        /// <summary>
        /// Confirms a User Password reset 
        /// </summary>
        /// <param name="passwordResetToken"></param>
        /// <param name="password"></param>
        /// <param name="passwordConfirm"></param>
        /// <returns></returns>
        public async Task<UserAuthModel?> ConfirmPasswordResetAsync(string passwordResetToken, string password, string passwordConfirm)
            => await App.Sdk.User.ConfirmPasswordResetAsync(passwordResetToken, password, passwordConfirm);

        /// <summary>
        /// Sends a User verification Request
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task RequestVerificationAsync(string email)
            => await App.Sdk.User.RequestVerificationAsync(email);

        /// <summary>
        /// Confirms a User verification
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<UserAuthModel?> ConfirmVerificationAsync(string token)
            => await App.Sdk.User.ConfirmVerificationAsync(token);

        /// <summary>
        /// Requests a change of email user
        /// </summary>
        /// <param name="newEmail"></param>
        /// <returns></returns>
        public async Task RequestEmailChangeAsync(string newEmail)
            => await App.Sdk.User.RequestEmailChangeAsync(newEmail);

        /// <summary>
        /// Confirms the change of email
        /// </summary>
        /// <param name="emailChangeToken"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        public async Task<UserAuthModel?> ConfirmEmailChangeAsync(string emailChangeToken, string userPassword)
            => await App.Sdk.User.ConfirmEmailChangeAsync(emailChangeToken, userPassword);

        /// <summary>
        /// Gets available externa Authentication methods for the user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ExternalAuthModel>?> GetExternalAuthenticationMethodsAsync(string userId)
            => await App.Sdk.User.GetExternalAuthenticationMethods(userId);
    }
}
