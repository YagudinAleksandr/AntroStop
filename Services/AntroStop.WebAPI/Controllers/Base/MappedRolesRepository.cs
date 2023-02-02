using AntroStop.DAL.Entities;
using AntroStop.Domain.Base.Models;
using AntroStop.Interfaces.Base.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AntroStop.WebAPI.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class MappedRolesRepository<T, TBase> : ControllerBase where TBase : Role where T : RolesInfo
    {
        private readonly IIntRepository<TBase> repository;
        private readonly IMapper mapper;

        public MappedRolesRepository(IIntRepository<TBase> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        protected virtual TBase GetBase(T item) => mapper.Map<TBase>(item);
        protected virtual T GetItem(TBase item) => mapper.Map<T>(item);

        protected virtual IEnumerable<TBase> GetBase(IEnumerable<T> items) => mapper.Map<IEnumerable<TBase>>(items);
        protected virtual IEnumerable<T> GetItem(IEnumerable<TBase> items) => mapper.Map<IEnumerable<T>>(items);

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id) => GetItem(await repository.Get(id)) is { } item ? Ok(item) : NotFound();

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll() => Ok(GetItem(await repository.GetAll()));
    }
}
