using AntroStop.DAL.Entities;
using AntroStop.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AntroStop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository<User> users;
        public UsersController(IUsersRepository<User> users) => this.users = users;

        //======================================================================================

        // Колличественный вывод

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> GetViolationsCount() => Ok(await users.Count());

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() => Ok(await users.GetAll());

        [HttpGet("items[[{Skip:int}:{Count:int}]]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<User>>> Get(int Skip, int Count) => Ok(await users.Get(Skip, Count));

       

        //======================================================================================

        //=======================================================================================

        //CRUD

        [HttpGet("exist/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(bool))]
        public async Task<IActionResult> Exist(string id) => await users.ExistID(id) ? Ok(true) : NotFound(false);

        

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id) => await users.Get(id) is { } item ? Ok(item) : NotFound();

        

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add(User item)
        {
            var result = await users.Add(item);

            return CreatedAtAction(nameof(Get), new { id = result.ID }, result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(User item)
        {
            var result = await users.Update(item);

            if (result is null)
                return NotFound(item);
            return AcceptedAtAction(nameof(Get), new { id = result.ID }, result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            if (await users.Delete(id) is not { } result)
                return NotFound(id);
            return Ok(result);
        }

        //=======================================================================================
    }
}
