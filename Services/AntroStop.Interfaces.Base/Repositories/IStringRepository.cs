﻿using AntroStop.Interfaces.Base.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace AntroStop.Interfaces.Base.Repositories
{
    public interface IStringRepository<T> where T : IStringEntity
    {
        Task<bool> ExistID(string Username, CancellationToken Cancel = default);
        Task<int> Count(CancellationToken Cancel = default);
        Task<IEnumerable<T>> GetAll(CancellationToken Cancel = default);
        Task<T> Add(T entity, CancellationToken Cancel = default);
        Task<T> Update(T entity, CancellationToken Cancel = default);
        Task<T> Delete(int ID, CancellationToken Cancel = default);
        Task<T> Get(int ID, CancellationToken Cancel = default);
    }
}