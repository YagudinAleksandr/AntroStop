using AntroStop.Interfaces.Base.Entities;
using AntroStop.Interfaces.Base.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace AntroStop.Interfaces.Repositories
{
    public interface IViolationsRepository<T> : IGuidRepository<T> where T : IGuidEntity
    {
        Task<IEnumerable<T>> GetAllByID(string Id, CancellationToken Cancel = default);
        Task<IEnumerable<T>> GetAllByStatus(string Status, CancellationToken Cancel = default);
        Task<int> GetCountByStatus(string Status, CancellationToken Cancel = default);
    }
}
