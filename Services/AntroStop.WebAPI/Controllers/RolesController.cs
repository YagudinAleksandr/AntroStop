using AntroStop.DAL.Entities;
using AntroStop.Interfaces.Base.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AntroStop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IIntRepository<Role> roles;
        public RolesController(IIntRepository<Role> roles)
        {
            this.roles = roles;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id) => await roles.Get(id) is { } item ? Ok(item) : NotFound();

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() => Ok(await roles.GetAll());
    }
}
