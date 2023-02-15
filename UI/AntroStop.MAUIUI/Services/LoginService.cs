using AntroStop.Domain.Base.AuthModels;
using AntroStop.Interfaces.WebRepositories;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AntroStop.MAUIUI.Services
{
    internal class LoginService : IAuthenticationService
    {
        private readonly HttpClient client;
        private readonly JsonSerializerOptions options;

        public LoginService(HttpClient client)
        {
            this.client = client;
            this.options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<AuthResponseDto> Login(UserForAuthenticationDto userForAuthentication)
        {
            var content = JsonSerializer.Serialize(userForAuthentication);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var authResult = await client.PostAsync("api/MappedAuth/login", bodyContent);
            var authContent = await authResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AuthResponseDto>(authContent, options);
            if (!authResult.IsSuccessStatusCode)
                return result;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
            return new AuthResponseDto { IsAuthSuccessful = true };
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
