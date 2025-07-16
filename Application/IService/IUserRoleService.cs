using Application.Dtos.Role;
using Application.Dtos.UserRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IUserRoleService
    {

        Task<UserRoleDto> CreateUserRole(CreateUserRole createUserRole );
        Task<UserRoleDto> UpdateUserRole(UpdateUserRole updateUserRole);

        Task<UserRoleDto> DeleteUserRole(string userId , string roleId);

        Task<IEnumerable<UserRoleDto>> GetAllUserRole();










    }
}
