using AntroStop.DAL.Entities;
using AntroStop.Domain.Base.Models;
using AntroStop.Interfaces.Repositories;
using AntroStop.WebAPI.Controllers.Base;
using AutoMapper;

namespace AntroStop.WebAPI.Controllers
{
    public class ElementsRepositoryController : MappedElementsController<ElementsInfo, Element>
    {
        public ElementsRepositoryController(IElementRepository<Element> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
