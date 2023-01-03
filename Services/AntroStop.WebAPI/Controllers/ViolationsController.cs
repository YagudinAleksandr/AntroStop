using AntroStop.DAL.Entities;
using AntroStop.Interfaces.Base.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AntroStop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViolationsController : ControllerBase
    {
        private readonly IGuidRepository<Violation> violation;
        public ViolationsController(IGuidRepository<Violation> violation)
        {
            this.violation = violation;
        }
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> GetViolationsCount() => Ok(await violation.Count());

        [HttpGet("exist/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(bool))]
        public async Task<IActionResult> Exist(Guid id) => await violation.ExistID(id) ? Ok(true) : NotFound(false);

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() => Ok(await violation.GetAll());

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id) => await violation.Get(id) is { } item ? Ok(item) : NotFound();

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add(Violation item)
        {
            var result = await violation.Add(item);

            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Violation item)
        {
            var result = await violation.Update(item);

            if (result is null)
                return NotFound(item);
            return AcceptedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (await violation.Delete(id) is not { } result)
                return NotFound(id);
            return Ok(result);
        }
    }
}
