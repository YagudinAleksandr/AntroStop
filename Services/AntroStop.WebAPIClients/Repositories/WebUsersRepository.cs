using AntroStop.Interfaces.Base.Entities;
using AntroStop.Interfaces.Base.Repositories;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AntroStop.WebAPIClients.Repositories
{
    public class WebUsersRepository<T> : IStringRepository<T> where T : IStringEntity
    {
        private readonly HttpClient client;

        public WebUsersRepository(HttpClient client) => this.client = client;

        public Task<T> Add(T entity, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Count(CancellationToken Cancel = default) =>
            await client.GetFromJsonAsync<int>("count", Cancel).ConfigureAwait(false);

        public Task<T> Delete(string ID, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistID(string ID, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> Get(string ID, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> Get(int skip, int count, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAll(CancellationToken Cancel = default) =>
            await client.GetFromJsonAsync<IEnumerable<T>>("", Cancel).ConfigureAwait(false);

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
