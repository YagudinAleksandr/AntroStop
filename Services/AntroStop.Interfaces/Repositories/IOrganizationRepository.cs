using AntroStop.Interfaces.Base.Entities;
using AntroStop.Interfaces.Base.Repositories;

namespace AntroStop.Interfaces.Repositories
{
    public interface IOrganizationRepository<T> : IIntRepository<T> where T : IIntEntity
    {
    }
}
