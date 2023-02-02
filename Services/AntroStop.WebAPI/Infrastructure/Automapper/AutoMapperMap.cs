﻿using AntroStop.DAL.Entities;
using AntroStop.Domain.Base.Models;
using AntroStop.Domain.Base.Models.Users;
using AutoMapper;

namespace AntroStop.WebAPI.Infrastructure.Automapper
{
    public class AutoMapperMap : Profile
    {
        public AutoMapperMap() 
        {
            CreateMap<ViolationsInfo, Violation>()
                .ReverseMap();

            CreateMap<UsersInfo, User>()
                .ReverseMap();

            CreateMap<RolesInfo, Role>()
                .ReverseMap();
        }
    }
}
