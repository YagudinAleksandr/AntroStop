using AntroStop.Interfaces.Base.Entities;
using AntroStop.Interfaces.Base.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace AntroStop.Interfaces.Repositories
{
    public interface IViolationRepository<T> : IGuidRepository<T> where T : IGuidEntity
    {
        Task<IEnumerable<T>> GetAllByID(string Id, CancellationToken Cancel = default);
        Task<IEnumerable<T>> GetAllByStatus(string Status, CancellationToken Cancel = default);
    }
}
