using AntroStop.DAL.Entities;
using AntroStop.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AntroStop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationUsersController : ControllerBase
    {
        private readonly IOrganizationUserRepository<OrganizationUser> organizationUsers;
        public OrganizationUsersController(IOrganizationUserRepository<OrganizationUser> organizationUsers)
        {
            this.organizationUsers = organizationUsers;
        }

        //======================================================================================

        // Колличественный вывод

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> GetViolationsCount() => Ok(await organizationUsers.Count());

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() => Ok(await organizationUsers.GetAll());

        //=======================================================================================

        //=======================================================================================

        //CRUD

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add(OrganizationUser item)
        {
            var result = await organizationUsers.Add(item);

            return CreatedAtAction(nameof(Get), new { id = result.ID }, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id) => await organizationUsers.Get(id) is { } item ? Ok(item) : NotFound();

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (await organizationUsers.Delete(id) is not { } result)
                return NotFound(id);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(OrganizationUser item)
        {
            var result = await organizationUsers.Update(item);

            if (result is null)
                return NotFound(item);
            return AcceptedAtAction(nameof(Get), new { id = result.ID }, result);
        }

        //=======================================================================================
    }
}
