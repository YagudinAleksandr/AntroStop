using AntroStop.Interfaces.Base.Entities;
using AntroStop.Interfaces.Base.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace AntroStop.Interfaces.Repositories
{
    public interface IElemetAdditionalRepository<T> : IGuidRepository<T> where T : IGuidEntity
    {
        Task<IEnumerable<T>> GetAllByID(Guid Id, CancellationToken Cancel = default);
    }
}
