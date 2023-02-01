using AntroStop.DAL.Entities;
using AntroStop.Domain.Base.Models.Users;
using AntroStop.Interfaces.Base.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntroStop.WebAPI.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class MappedUsersController<T,TBase> : ControllerBase where TBase : User where T : UsersInfo
    {
        private readonly IStringRepository<TBase> repository;
        private readonly IMapper mapper;

        public MappedUsersController(IStringRepository<TBase> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        protected virtual TBase GetBase(T item) => mapper.Map<TBase>(item);
        protected virtual T GetItem(TBase item) => mapper.Map<T>(item);

        protected virtual IEnumerable<TBase> GetBase(IEnumerable<T> items) => mapper.Map<IEnumerable<TBase>>(items);
        protected virtual IEnumerable<T> GetItem(IEnumerable<TBase> items) => mapper.Map<IEnumerable<T>>(items);

        //======================================================================================

        // Колличественный вывод

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> GetViolationsCount() => Ok(await repository.Count());

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() => Ok(GetItem(await repository.GetAll()));

        /*
        [HttpGet("items[[{Skip:int}:{Count:int}]]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<User>>> Get(int Skip, int Count) => Ok(await repository.Get(Skip, Count));

        [HttpGet("/page[[{PageIndex:int}:{PageSize:int}]]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IPaget<User>>> GetPage(int PageIndex, int PageSize)
        {
            var result = await repository.GetPage(PageIndex, PageSize);

            return result.Items.Any() ? Ok(result) : NotFound(result);
        }*/

        //======================================================================================

        //=======================================================================================

        //CRUD

        [HttpGet("exist/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(bool))]
        public async Task<IActionResult> Exist(string id) => await repository.ExistID(id) ? Ok(true) : NotFound(false);



        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id) => GetItem(await repository.Get(id)) is { } item ? Ok(item) : NotFound();



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add(T item)
        {
            var result = await repository.Add(GetBase(item));

            return CreatedAtAction(nameof(Get), new { id = result.ID }, result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(T item)
        {
            var result = await repository.Update(GetBase(item));

            if (result is null)
                return NotFound(item);
            return AcceptedAtAction(nameof(Get), new { id = result.ID }, result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            if (await repository.Delete(id) is not { } result)
                return NotFound(id);
            return Ok(result);
        }

        //=======================================================================================
    }
}
