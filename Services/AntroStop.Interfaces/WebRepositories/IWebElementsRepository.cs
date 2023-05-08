using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AntroStop.Interfaces.WebRepositories
{
    public interface IWebElementsRepository<T> where T : class
    {
        Task<string> UploadProductImage(MultipartFormDataContent content);
        Task<IEnumerable<T>> GetAllByID(string Id, CancellationToken Cancel = default);
        Task<T> Add(T entity, CancellationToken Cancel = default);
    }
}
