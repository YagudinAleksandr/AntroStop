using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AntroStop.BlazorUI.Infrastructure.Extensions
{
    internal static class ServiceExtensions
    {
        public static IHttpClientBuilder AddApi<IInterface, IClient>(this IServiceCollection services, string address)
            where IInterface : class where IClient : class, IInterface => services
            .AddHttpClient<IInterface, IClient>(
                (host, client) => client.BaseAddress = new($"{host.GetRequiredService<IWebAssemblyHostEnvironment>().BaseAddress}{ address}"));
    }
}
