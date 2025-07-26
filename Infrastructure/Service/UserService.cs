using Application.Dtos.LogIn;
using Application.IRepository;
using Application.IService;
using Application.LDAP;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class UserService : IUserService
    {
        private readonly IAppRepository<User> _repo;
        private readonly IMapper _mapper;

        public UserService(IAppRepository<User> app ,IMapper mapper )
        {
            _repo = app;
            _mapper = mapper;
             
        }

        //public async Task<UserDto> DeleteUser(string UserId)
        //{

        //}

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
           return _mapper.Map<IEnumerable<UserDto>>(await _repo.GetAllAsync());

        }

        public async Task<UserDto> GetUserById(string userID)
        {
            return _mapper.Map<UserDto>(await _repo.GetById(userID));
        }

        public async Task<UserDto> InsertUser(LdapUser user)
        {
            var usr = _mapper.Map<User>(user);
            await _repo.Insertasync(usr);
            return _mapper.Map<UserDto>(usr);
        }

      public  async Task<UserDto> GetUserByEmail(string email)
        {
           var user =  await _repo.GetAllAsync();
           var us = user.FirstOrDefault(x=>x.Email==email);
            return _mapper.Map<UserDto>(us);
        }

        //Task<User> IUserService.GetUserByEmail(string email)
        //{
        //    return GetUserByEmail(email);
        //}
    }
}
