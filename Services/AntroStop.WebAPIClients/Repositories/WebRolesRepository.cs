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
    public class WebRolesRepository<T> : IIntRepository<T> where T : IIntEntity
    {
        private readonly HttpClient client;

        public WebRolesRepository(HttpClient client) => this.client = client;

        public Task<T> Add(T entity, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> Count(CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> Delete(int ID, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistID(int ID, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public async Task<T> Get(int ID, CancellationToken Cancel = default) =>
            await client.GetFromJsonAsync<T>($"{ID}", Cancel).ConfigureAwait(false);

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
