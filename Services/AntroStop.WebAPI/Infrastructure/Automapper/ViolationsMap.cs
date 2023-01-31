using AntroStop.DAL.Entities;
using AutoMapper;

namespace AntroStop.WebAPI.Infrastructure.Automapper
{
    public class ViolationsMap : Profile
    {
        public ViolationsMap()
        {
            CreateMap<ViolationsMap, Violation>()
                .ReverseMap();
        }
    }
}
