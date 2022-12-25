using AntroStop.DAL.Entities;
using AntroStop.Interfaces.Base.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AntroStop.DAL.Repositories
{
    public class ElementRepository<T> : IGuidRepository<T> where T : Element, new()
    {
        public Task<T> Add(T entity, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> Count(CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> Delete(Guid ID, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistID(Guid ID, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> Get(Guid ID, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAll(CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> Update(T entity, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }
    }
}
