﻿using AntroStop.Interfaces.Base.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using AntroStop.Domain.Pagination.Paging;
using AntroStop.Domain.Pagination.RequestFeatures;

namespace AntroStop.Interfaces.Base.Repositories
{
    public interface IStringRepository<T> where T : IStringEntity
    {
        Task<bool> ExistID(string ID, CancellationToken Cancel = default);
        Task<int> Count(CancellationToken Cancel = default);
        Task<IEnumerable<T>> GetAll(CancellationToken Cancel = default);
        Task<T> Add(T entity, CancellationToken Cancel = default);
        Task<T> Update(T entity, CancellationToken Cancel = default);
        Task<T> Delete(string ID, CancellationToken Cancel = default);
        Task<T> Get(string ID, CancellationToken Cancel = default);
        Task<IEnumerable<T>> Get(int skip, int count, CancellationToken Cancel = default);
        Task<PagedList<T>> GetPage(PageParametrs productParameters, CancellationToken Cancel = default);
    }
}
