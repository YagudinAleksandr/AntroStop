using AntroStop.Domain.Base.Models.Users;
using AntroStop.Domain.Pagination.Features;
using AntroStop.Domain.Pagination.RequestFeatures;
using AntroStop.Interfaces.WebRepositories;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AntroStop.WebAPIClients.Repositories
{
    public class WebUsersRepository<T> : IWebUsersRepository<T> where T : UsersInfo
    {
        private readonly HttpClient client;
        private readonly JsonSerializerOptions options;

        public WebUsersRepository(HttpClient client)
        {
            this.client = client;
            options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<T> Add(T entity, CancellationToken Cancel = default)
        {
            var response = await client.PostAsJsonAsync("", entity, Cancel).ConfigureAwait(false);
            var result = await response
               .EnsureSuccessStatusCode()
               .Content
               .ReadFromJsonAsync<T>(cancellationToken: Cancel)
               .ConfigureAwait(false);
            return result;
        }

        public async Task<int> Count(CancellationToken Cancel = default) =>
            await client.GetFromJsonAsync<int>("count", Cancel).ConfigureAwait(false);

        public async Task<T> Delete(string ID, CancellationToken Cancel = default)
        {
            var response = await client.DeleteAsync($"{ID}", Cancel).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.NotFound)
                return default;

            var result = await response
               .EnsureSuccessStatusCode()
               .Content
               .ReadFromJsonAsync<T>(cancellationToken: Cancel)
               .ConfigureAwait(false);

            return result;
        }

        public Task<bool> ExistID(string ID, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public async Task<T> Get(string ID, CancellationToken Cancel = default) =>
            await client.GetFromJsonAsync<T>($"{ID}", Cancel).ConfigureAwait(false);

        public async Task<T> Update(T entity, CancellationToken Cancel = default)
        {
            var response = await client.PutAsJsonAsync("", entity, Cancel).ConfigureAwait(false);
            var result = await response
               .EnsureSuccessStatusCode()
               .Content
               .ReadFromJsonAsync<T>(cancellationToken: Cancel)
               .ConfigureAwait(false);
            return result;
        }

        public async Task<PagingResponse<T>> GetPage(PageParametrs productParameters, CancellationToken Cancel = default)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageNumber"] = productParameters.PageNumber.ToString()
            };
            var response = await client.GetAsync(QueryHelpers.AddQueryString("", queryStringParam), Cancel).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<T>
            {
                Items = JsonSerializer.Deserialize<List<T>>(content, options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), options)
            };
            return pagingResponse;
        }
    }
}
