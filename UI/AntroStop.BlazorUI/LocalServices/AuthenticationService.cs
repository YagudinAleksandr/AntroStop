using AntroStop.BlazorUI.Providers;
using AntroStop.Domain.Base.AuthModels;
using AntroStop.Interfaces.WebRepositories;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AntroStop.BlazorUI.LocalServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient client;
        private readonly JsonSerializerOptions options;
        private readonly AuthenticationStateProvider authStateProvider;
        private readonly ILocalStorageService localStorage;

        public AuthenticationService(HttpClient client, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
        {
            this.client = client;
            this.options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            this.authStateProvider = authStateProvider;
            this.localStorage = localStorage;
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
            await localStorage.SetItemAsync("authToken", result.Token);
            ((AuthStateProvider)authStateProvider).NotifyUserAuthentication(userForAuthentication.Id);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
            return new AuthResponseDto { IsAuthSuccessful = true };
        }

        public async Task Logout()
        {
            await localStorage.RemoveItemAsync("authToken");
            ((AuthStateProvider)authStateProvider).NotifyUserLogout();
            client.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<RegistrationResponseDto> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var content = JsonSerializer.Serialize(userForRegistration);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var registrationResult = await client.PostAsync("api/MappedAuth/registration", bodyContent);
            var registrationContent = await registrationResult.Content.ReadAsStringAsync();

            if (!registrationResult.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<RegistrationResponseDto>(registrationContent, options);
                return result;
            }

            return new RegistrationResponseDto { IsSuccessfulRegistration = true };
        }
    }
}
