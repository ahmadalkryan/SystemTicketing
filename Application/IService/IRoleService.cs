using Application.Dtos.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IRoleService
    {
        Task<RoleDto> CreateRole(CreateRoleDto createRoleDto);
        Task<RoleDto> UpdateRole(UpdateRoleDto updateRoleDto);

        Task<RoleDto> DeleteRole(string roleId);
        Task<RoleDto> GetRoleById(string roleId);
        Task<IEnumerable<RoleDto>> GetAllRoles();
        Task<IEnumerable<RoleDto>>GetRoleByUserId(string userId);
    }
}
