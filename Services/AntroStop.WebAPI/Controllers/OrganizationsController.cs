using AntroStop.DAL.Entities;
using AntroStop.DAL.Repositories;
using AntroStop.Interfaces.Base.Repositories;
using AntroStop.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AntroStop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationRepository<Organization> organizations;
        public OrganizationsController(IOrganizationRepository<Organization> organizations)
        {
            this.organizations  = organizations;
        }

        //======================================================================================

        // Колличественный вывод

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> GetViolationsCount() => Ok(await organizations.Count());

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() => Ok(await organizations.GetAll());

        //=======================================================================================

        //=======================================================================================

        //CRUD

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add(Organization item)
        {
            var result = await organizations.Add(item);

            return CreatedAtAction(nameof(Get), new { id = result.ID }, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id) => await organizations.Get(id) is { } item ? Ok(item) : NotFound();

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            if (await organizations.Delete(id) is not { } result)
                return NotFound(id);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Organization item)
        {
            var result = await organizations.Update(item);

            if (result is null)
                return NotFound(item);
            return AcceptedAtAction(nameof(Get), new { id = result.ID }, result);
        }

        //=======================================================================================
    }
}
