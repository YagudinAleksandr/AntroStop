using AntroStop.DAL.Entities;
using AntroStop.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AntroStop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViolationsController : ControllerBase
    {
        private readonly IViolationRepository<Violation> violations;
        public ViolationsController(IViolationRepository<Violation> violations) => this.violations = violations;

        //======================================================================================

        // Колличественный вывод

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> GetViolationsCount() => Ok(await violations.Count());

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() => Ok(await violations.GetAll());

        [HttpGet("getbystatus/{status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByStatus(string status) => Ok(await violations.GetAllByStatus(status));

        [HttpGet("getbyuser/{userid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByUser(string userid) => Ok(await violations.GetAllByID(userid));

        [HttpGet("items[[{Skip:int}:{Count:int}]]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<User>>> Get(int Skip, int Count) => Ok(await violations.Get(Skip, Count));

        //=======================================================================================

        //=======================================================================================

        //CRUD

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add(Violation item)
        {
            var result = await violations.Add(item);

            return CreatedAtAction(nameof(Get), new { id = result.Id}, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id) => await violations.Get(Guid.Parse(id)) is { } item ? Ok(item) : NotFound();

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            if (await violations.Delete(Guid.Parse(id)) is not { } result)
                return NotFound(id);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Violation item)
        {
            var result = await violations.Update(item);

            if (result is null)
                return NotFound(item);
            return AcceptedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        //=======================================================================================
    }
}
