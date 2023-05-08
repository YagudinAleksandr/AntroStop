using AntroStop.DAL.Entities;
using AntroStop.Domain.Base.Models;
using AntroStop.Domain.Pagination.RequestFeatures;
using AntroStop.Interfaces.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AntroStop.WebAPI.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class MappedElementsController<T, TBase> : ControllerBase where TBase : Element where T : ElementsInfo
    {
        private readonly IElementRepository<TBase> repository;
        private readonly IMapper mapper;

        public MappedElementsController(IElementRepository<TBase> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        protected virtual TBase GetBase(T item) => mapper.Map<TBase>(item);
        protected virtual T GetItem(TBase item) => mapper.Map<T>(item);

        protected virtual IEnumerable<TBase> GetBase(IEnumerable<T> items) => mapper.Map<IEnumerable<TBase>>(items);
        protected virtual IEnumerable<T> GetItem(IEnumerable<TBase> items) => mapper.Map<IEnumerable<T>>(items);


        /*=========================================================================================================*/

        //Колличественный вывод

        [HttpGet("count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<IActionResult> GetCount() => Ok(await repository.Count());

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PageParametrs pageParametrs)
        {
            var items = await repository.GetPage(pageParametrs);

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(items.MetaData));

            return Ok(GetItem(items));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id) => GetItem(await repository.Get(Guid.Parse(id))) is { } item ? Ok(item) : NotFound();

        [HttpPost("upload")]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("StaticFiles", "Client");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok(dbPath);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPost]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Add(T item)
        {
            var result = await repository.Add(GetBase(item));

            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }
    }
}
