using AntroStop.DAL.Entities;
using AntroStop.Domain.Base.Models;
using AntroStop.Interfaces.Repositories;
using AntroStop.WebAPI.Controllers.Base;
using AutoMapper;

namespace AntroStop.WebAPI.Controllers
{

    public class ViolationsRepositoryController : MappedViolationsController<ViolationsInfo, Violation>
    {
        public ViolationsRepositoryController(IViolationRepository<Violation> repository, IMapper mapper) : base(repository, mapper)
        {

        }
    }
}
