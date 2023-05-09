using AntroStop.Domain.Base.Models;
using AntroStop.Interfaces.WebRepositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AntroStop.WebAPIClients.Repositories
{
    public class WebElementsRepository<T> : IWebElementsRepository<T> where T : ElementsInfo
    {
        #region поля
        private readonly HttpClient client;
        private readonly JsonSerializerOptions options;
        #endregion

        public WebElementsRepository(HttpClient client)
        {
            this.client = client;
            this.options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
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

        public async Task<IEnumerable<T>> GetAllByID(string Id, CancellationToken Cancel = default) =>
            await client.GetFromJsonAsync<IEnumerable<T>>($"allFilesByViolation/{Id}", Cancel).ConfigureAwait(false);

        public async Task<string> UploadProductImage(MultipartFormDataContent content)
        {
            var postResult = await client.PostAsync("upload", content).ConfigureAwait(false);
            var postContent = await postResult.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
            else
            {
                var imgUrl = Path.Combine("https://10.3.3.18:5004/", postContent);
                return imgUrl;
            }
        }
    }
}
