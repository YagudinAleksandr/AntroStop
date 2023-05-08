﻿using AntroStop.Domain;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace AntroStop.MauiUI.Providers
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationState anonymous;

        public AuthStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            this.anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await localStorage.GetItemAsync<string>("authToken");
            if (string.IsNullOrWhiteSpace(token))
                return anonymous;

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")));
        }

        public void NotifyUserAuthentication(string email)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, email) }, "jwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(anonymous);
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
