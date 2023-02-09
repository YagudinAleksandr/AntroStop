using AntroStop.Interfaces.Base.Entities;
using AntroStop.Interfaces.Base.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace AntroStop.Interfaces.Repositories
{
    public interface IUsersRepository<T> : IStringRepository<T> where T :IStringEntity
    {
        Task<T> GetByData(string Id, string password, CancellationToken Cancel = default);
    }
}
