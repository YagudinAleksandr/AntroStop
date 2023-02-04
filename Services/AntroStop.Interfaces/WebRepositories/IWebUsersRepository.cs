using System.Threading.Tasks;
using System.Threading;
using AntroStop.Domain.Pagination.RequestFeatures;
using AntroStop.Domain.Pagination.Features;

namespace AntroStop.Interfaces.WebRepositories
{
    public interface IWebUsersRepository<T> where T : class
    {
        Task<bool> ExistID(string ID, CancellationToken Cancel = default);
        Task<int> Count(CancellationToken Cancel = default);
        Task<T> Add(T entity, CancellationToken Cancel = default);
        Task<T> Update(T entity, CancellationToken Cancel = default);
        Task<T> Delete(string ID, CancellationToken Cancel = default);
        Task<T> Get(string ID, CancellationToken Cancel = default);
        Task<PagingResponse<T>> GetPage(PageParametrs productParameters, CancellationToken Cancel = default);
    }
}
