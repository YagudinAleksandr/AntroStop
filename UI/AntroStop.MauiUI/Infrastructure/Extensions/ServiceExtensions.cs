using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace AntroStop.MauiUI.Infrastructure.Extensions
{
    internal static class ServiceExtensions
    {
        public static IHttpClientBuilder AddApi<IInterface, IClient>(this IServiceCollection services, string address)
            where IInterface : class where IClient : class, IInterface => services
            .AddHttpClient<IInterface, IClient>(
                (host, client) => client.BaseAddress = new($"http://10.3.3.18:5002/{address}"));
    }
}
