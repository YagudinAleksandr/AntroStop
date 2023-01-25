using AntroStop.Interfaces.Base.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace AntroStop.Interfaces.Base.Repositories
{
    public interface IGuidRepository<T> where T : IGuidEntity
    {
        Task<bool> ExistID(Guid ID, CancellationToken Cancel = default);
        Task<int> Count(CancellationToken Cancel = default);
        Task<IEnumerable<T>> GetAll(CancellationToken Cancel = default);
        Task<T> Add(T entity, CancellationToken Cancel = default);
        Task<T> Update(T entity, CancellationToken Cancel = default);
        Task<T> Delete(Guid ID, CancellationToken Cancel = default);
        Task<T> Get(Guid ID, CancellationToken Cancel = default);
        Task<IEnumerable<T>> Get(int skip, int count, CancellationToken Cancel = default);
    }
}
