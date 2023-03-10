using AntroStop.DAL.Entities;
using AntroStop.Domain.Base.Models;
using AntroStop.Interfaces.Repositories;
using AntroStop.WebAPI.Controllers.Base;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AntroStop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViolationsRepositoryController : MappedViolationsController<ViolationsInfo, Violation>
    {
        public ViolationsRepositoryController(IViolationsRepository<Violation> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
