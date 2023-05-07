using AntroStop.Domain.Pagination.Features;
using AntroStop.Domain.Pagination.RequestFeatures;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System;

namespace AntroStop.Interfaces.WebRepositories
{
    public interface IWebViolationsRepository<T> where T : class
    {
        Task<bool> ExistID(Guid ID, CancellationToken Cancel = default);
        Task<int> Count(CancellationToken Cancel = default);
        Task<IEnumerable<T>> GetAllByID(string Id, CancellationToken Cancel = default);
        Task<IEnumerable<T>> GetAllByStatus(string Status, CancellationToken Cancel = default);
        Task<int> GetCountByStatus(string Status, CancellationToken Cancel = default);
        Task<T> Add(T entity, CancellationToken Cancel = default);
        Task<T> Update(T entity, CancellationToken Cancel = default);
        Task<T> Delete(string ID, CancellationToken Cancel = default);
        Task<T> Get(string ID, CancellationToken Cancel = default);
        Task<PagingResponse<T>> GetPage(PageParametrs productParameters, CancellationToken Cancel = default);
    }
}
