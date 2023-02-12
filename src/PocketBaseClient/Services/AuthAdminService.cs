// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase

using pocketbase_csharp_sdk.Models.Auth;

namespace PocketBaseClient.Services
{
    /// <summary>
    /// Class that encapsulates Authentication as Admin operations
    /// </summary>
    public class AuthAdminService : ServiceBase
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="app"></param>
        public AuthAdminService(PocketBaseClientApplication app) : base(app) { }

        /// <summary>
        /// Authenticate as Admin with identifier and Password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<AdminAuthModel?> AuthenticateWithPasswordAsync(string email, string password)
            => await App.Sdk.Admin.AuthenticateWithPasswordAsync(email, password);

        /// <summary>
        /// Request an Admin Password reset
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task RequestPasswordResetAsync(string email)
            => await App.Sdk.Admin.RequestPasswordResetAsync(email);

        /// <summary>
        /// Confirm an Admin Password reset
        /// </summary>
        /// <param name="passwordResetToken"></param>
        /// <param name="password"></param>
        /// <param name="passwordConfirm"></param>
        /// <returns></returns>
        public async Task<AdminAuthModel?> ConfirmPasswordResetAsync(string passwordResetToken, string password, string passwordConfirm)
            => await App.Sdk.Admin.ConfirmPasswordResetAsync(passwordResetToken, password, passwordConfirm);

    }
}
