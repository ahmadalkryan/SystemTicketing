using Application.Dtos.UserRole;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class UserRoleProfile:Profile
    {
        public UserRoleProfile()
        {
            CreateMap<UserRole,UserRoleDto>();
            CreateMap<CreateUserRole, UserRole>();
            CreateMap<UpdateUserRole,UserRole >();
        }
    }
}
