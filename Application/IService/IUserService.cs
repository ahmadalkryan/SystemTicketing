using Application.Dtos.LogIn;
using Application.LDAP;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsers();
        Task<UserDto> GetUserById(string userID);

        Task<UserDto> GetUserByEmail(string email);

        Task<UserDto> InsertUser(LdapUser user);

       // Task<UserDto> DeleteUser();
           

    }
}
