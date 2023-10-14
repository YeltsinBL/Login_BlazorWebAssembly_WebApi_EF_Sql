using LoginApplication.Shared;

namespace LoginApplication.Client.Service
{
    public interface IAuthService
	{
        Task<RegisteResult> Register(RegisterModel registerModel);
        Task<LoginResult> Login(LoginModel loginModel);
        Task Logout();
    }
}

