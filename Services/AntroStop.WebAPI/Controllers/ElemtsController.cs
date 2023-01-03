using AntroStop.DAL.Entities;
using AntroStop.Interfaces.Base.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using AntroStop.Interfaces.Repositories;

namespace AntroStop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElemtsController : ControllerBase
    {
        private readonly IElemetAdditionalRepository<Element> element;
        public ElemtsController(IElemetAdditionalRepository<Element> element)
        {
            this.element = element;
        }
        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> GetViolationsCount() => Ok(await element.Count());

        [HttpGet("exist/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(bool))]
        public async Task<IActionResult> Exist(Guid id) => await element.ExistID(id) ? Ok(true) : NotFound(false);

        [HttpGet("all/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(Guid id) => Ok(await element.GetAllByID(id));

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id) => await element.Get(id) is { } item ? Ok(item) : NotFound();

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add(Element item)
        {
            var result = await element.Add(item);

            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Element item)
        {
            var result = await element.Update(item);

            if (result is null)
                return NotFound(item);
            return AcceptedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (await element.Delete(id) is not { } result)
                return NotFound(id);
            return Ok(result);
        }
    }
}
