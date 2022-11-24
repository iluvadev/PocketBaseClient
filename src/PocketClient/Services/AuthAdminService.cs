using pocketbase_csharp_sdk.Models.Auth;

namespace PocketClient.Services
{
    public class AuthAdminService : ServiceBase
    {
        public AuthAdminService(PocketClientAppication app) : base(app) { }

        public async Task<AdminAuthModel?> AuthenticateWithPassword(string email, string password)
            => await App.Sdk.Admin.AuthenticateWithPassword(email, password);

        public async Task RequestPasswordResetAsync(string email)
            => await App.Sdk.Admin.RequestPasswordResetAsync(email);

        public async Task<AdminAuthModel?> ConfirmPasswordResetAsync(string passwordResetToken, string password, string passwordConfirm)
            => await App.Sdk.Admin.ConfirmPasswordResetAsync(passwordResetToken, password, passwordConfirm);

    }
}
