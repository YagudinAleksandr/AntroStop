using AntroStop.DAL.Entities;
using AntroStop.Domain.Base.Models;
using AutoMapper;

namespace AntroStop.WebAPI.Infrastructure.Automapper
{
    public class AutoMapperMap : Profile
    {
        public AutoMapperMap() 
        {
            CreateMap<ViolationsInfo, Violation>()
                .ReverseMap();
        }
    }
}
