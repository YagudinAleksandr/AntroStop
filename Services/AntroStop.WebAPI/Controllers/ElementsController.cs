using AntroStop.DAL.Entities;
using AntroStop.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AntroStop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElementsController : ControllerBase
    {
        private readonly IElementRepository<Element> elements;
        public ElementsController(IElementRepository<Element> elements) => this.elements = elements;

        //======================================================================================

        // Колличественный вывод

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> GetViolationsCount() => Ok(await elements.Count());

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() => Ok(await elements.GetAll());

        [HttpGet("getbyuser/{violationid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByUser(string violationid) => Ok(await elements.GetAllByID(Guid.Parse(violationid)));

        //======================================================================================

        //=======================================================================================

        //CRUD

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Upload(Element item)
        {
            var result = await elements.Add(item);

            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id) => await elements.Get(Guid.Parse(id)) is { } item ? Ok(item) : NotFound();

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            if (await elements.Delete(Guid.Parse(id)) is not { } result)
                return NotFound(id);
            return Ok(result);
        }



        //=======================================================================================
    }
}
