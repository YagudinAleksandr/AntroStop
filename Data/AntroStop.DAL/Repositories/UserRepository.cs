using AntroStop.DAL.Entities;
using AntroStop.Interfaces.Base.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AntroStop.DAL.Repositories
{
    public class UserRepository<T> : IStringRepository<T> where T : User, new()
    {
        public Task<T> Add(T entity, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<int> Count(CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> Delete(int ID, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistID(string Username, CancellationToken Cancel = default)
        {
            throw new NotImplementedException();
        }

        public Task<T> Get(int ID, CancellationToken Cancel = default)
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
