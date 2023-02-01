using AntroStop.DAL.Entities;
using AntroStop.Domain.Base.Models.Users;
using AntroStop.Interfaces.Base.Repositories;
using AntroStop.WebAPI.Controllers.Base;
using AutoMapper;

namespace AntroStop.WebAPI.Controllers
{
    public class UsersRepositoryController : MappedUsersController<UsersInfo, User>
    {
        public UsersRepositoryController(IStringRepository<User> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
