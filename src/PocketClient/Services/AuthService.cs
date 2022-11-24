namespace PocketClient.Services
{
    public class AuthService: ServiceBase
    {
        private AuthAdminService? _Admin = null;
        public AuthAdminService Admin => _Admin ??= new AuthAdminService(App);

        private AuthUserService? _User = null;
        public AuthUserService User => _User ??= new AuthUserService(App);

        public AuthService(PocketClientAppication app) : base(app) { }
    }
}
