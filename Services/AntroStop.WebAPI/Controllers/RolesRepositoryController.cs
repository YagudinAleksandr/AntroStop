using AntroStop.DAL.Entities;
using AntroStop.Domain.Base.Models;
using AntroStop.Interfaces.Base.Repositories;
using AntroStop.WebAPI.Controllers.Base;
using AutoMapper;

namespace AntroStop.WebAPI.Controllers
{
    public class RolesRepositoryController : MappedRolesRepository<RolesInfo, Role>
    {
        public RolesRepositoryController(IIntRepository<Role> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
