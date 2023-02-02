using AntroStop.DAL.Entities;
using AntroStop.Interfaces.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using AntroStop.Domain.Base.Models;

namespace AntroStop.WebAPI.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class MappedViolationsController<T, TBase> : ControllerBase where TBase : Violation where T : ViolationsInfo
    {
        private readonly IViolationRepository<TBase> repository;
        private readonly IMapper mapper;

        public MappedViolationsController(IViolationRepository<TBase> repository, IMapper mapper)
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

        [HttpGet("countByStatus/{status}")]
        public async Task<IActionResult> GetViolationsCountByStatus(string status) => Ok(await repository.GetCountByStatus(status));

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() => Ok(GetItem(await repository.GetAll()));

        [HttpGet("getbyuser/{userid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByUser(string userid) => Ok(GetItem(await repository.GetAllByID(userid)));

        //======================================================================================

        //=======================================================================================

        //CRUD

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add(T item)
        {
            var result = await repository.Add(GetBase(item));

            return CreatedAtAction(nameof(Get), new { id = result.Id }, GetItem(result));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id) => GetItem(await repository.Get(Guid.Parse(id))) is { } item ? Ok(item) : NotFound();

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            if (await repository.Delete(Guid.Parse(id)) is not { } result)
                return NotFound(id);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(T item)
        {
            var result = await repository.Update(GetBase(item));

            if (result is null)
                return NotFound(item);
            return AcceptedAtAction(nameof(Get), new { id = result.Id }, GetItem(result));
        }
        //=======================================================================================
    }
}
