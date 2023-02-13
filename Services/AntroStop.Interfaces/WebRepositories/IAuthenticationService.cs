using AntroStop.Domain.Base.AuthModels;
using System.Threading.Tasks;

namespace AntroStop.Interfaces.WebRepositories
{
    public interface IAuthenticationService
    {
        Task<RegistrationResponseDto> RegisterUser(UserForRegistrationDto userForRegistration);
        Task<AuthResponseDto> Login(UserForAuthenticationDto userForAuthentication);
        Task Logout();
    }
}
