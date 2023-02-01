using AntroStop.Interfaces.Base.Entities;
using AntroStop.Interfaces.Base.Repositories;
using AntroStop.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AntroStop.WebAPIClients.Repositories
{
    public class WebViolationsRepository<T> : IViolationRepository<T> where T : IGuidEntity
    {
        private readonly HttpClient client;

        public WebViolationsRepository(HttpClient client) => this.client = client;

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

        public Task<T> Delete(Guid ID, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistID(Guid ID, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public async Task<T> Get(Guid ID, CancellationToken Cancel = default) =>
            await client.GetFromJsonAsync<T>($"{ID}", Cancel).ConfigureAwait(false);

        public Task<IEnumerable<T>> Get(int skip, int count, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAll(CancellationToken Cancel = default) =>
            await client.GetFromJsonAsync<IEnumerable<T>>("", Cancel).ConfigureAwait(false);

        public Task<IEnumerable<T>> GetAllByID(string Id, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllByStatus(string Status, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetCountByStatus(string Status, CancellationToken Cancel = default) =>
            await client.GetFromJsonAsync<int>($"countByStatus/{Status}", Cancel).ConfigureAwait(false);

        public Task<IPaget<T>> GetPage(int PageIndex, int PageSize, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> Update(T entity, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }
    }
}
