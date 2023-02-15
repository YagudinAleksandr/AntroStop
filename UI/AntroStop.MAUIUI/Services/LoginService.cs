using AntroStop.Domain.Base.AuthModels;
using AntroStop.Interfaces.WebRepositories;

namespace AntroStop.MAUIUI.Services
{
    internal class LoginService : IAuthenticationService
    {
        public Task<AuthResponseDto> Login(UserForAuthenticationDto userForAuthentication)
        {
            throw new NotImplementedException();
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public Task<RegistrationResponseDto> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            throw new NotImplementedException();
        }
    }
}
