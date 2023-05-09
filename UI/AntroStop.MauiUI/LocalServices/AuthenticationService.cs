using AntroStop.MauiUI.Providers;
using AntroStop.Domain.Base.AuthModels;
using AntroStop.Interfaces.WebRepositories;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Net.Http.Formatting;

namespace AntroStop.MauiUI.LocalServices
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient client;
        private readonly JsonSerializerOptions options;
        private readonly AuthenticationStateProvider authStateProvider;
        private readonly ILocalStorageService localStorage;

        public AuthenticationService(HttpClient client, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
        {
            this.client = new HttpClient();
            this.options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            this.authStateProvider = authStateProvider;
            this.localStorage = localStorage;
        }

        public async Task<AuthResponseDto> Login(UserForAuthenticationDto userForAuthentication)
        {
            var content = JsonSerializer.Serialize(userForAuthentication);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var authResult = await client.PostAsync("http://10.3.3.18:5002/api/MappedAuth/login", bodyContent).ConfigureAwait(false);
            var authContent = await authResult.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = JsonSerializer.Deserialize<AuthResponseDto>(authContent, options);
            if (!authResult.IsSuccessStatusCode)
                return result;
            await localStorage.SetItemAsync("authToken", result.Token).ConfigureAwait(false);
            ((AuthStateProvider)authStateProvider).NotifyUserAuthentication(userForAuthentication.Id);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
            return new AuthResponseDto { IsAuthSuccessful = true };
            
        }

        public async Task Logout()
        {
            await localStorage.RemoveItemAsync("authToken").ConfigureAwait(false);
            ((AuthStateProvider)authStateProvider).NotifyUserLogout();
            client.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<RegistrationResponseDto> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            var content = JsonSerializer.Serialize(userForRegistration);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var registrationResult = await client.PostAsync("http://10.3.3.18:5002/api/MappedAuth/registration", bodyContent).ConfigureAwait(false);
            var registrationContent = await registrationResult.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!registrationResult.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<RegistrationResponseDto>(registrationContent, options);
                return result;
            }

            return new RegistrationResponseDto { IsSuccessfulRegistration = true };
        }
    }
}
