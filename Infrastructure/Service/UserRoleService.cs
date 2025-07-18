using Application.Dtos.UserRole;
using Application.IRepository;
using Application.IService;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IAppRepository<UserRole> _repo;
        private readonly IMapper _mapper;

        public UserRoleService(IAppRepository<UserRole> appRepository ,IMapper mapper)
        {
            _mapper = mapper;
            _repo = appRepository;
        }
        public async Task<UserRoleDto> CreateUserRole(CreateUserRole createUserRole)
        {
            var ur = _mapper.Map<UserRole>(createUserRole);
            await _repo.Insertasync(ur);
            return _mapper.Map<UserRoleDto>(ur);
        }

        public async Task<UserRoleDto> DeleteUserRole(string userId, string roleId)
        {
            var ur = await _repo.GetAllAsync();
              var u = ur.FirstOrDefault(x=>x.UserId==userId && x.RoleId==roleId);
     
            await _repo.RemoveAsync(u);

            return _mapper.Map<UserRoleDto>(ur);
        }

        public async Task<IEnumerable<UserRoleDto>> GetAllUserRole() => _mapper.Map<IEnumerable<UserRoleDto>>((await _repo.GetAllAsync()));
       
        public async Task<UserRoleDto> UpdateUserRole(UpdateUserRole updateUserRole)
        {
            var ur = _mapper?.Map<UserRole>(updateUserRole);
            await _repo.UpdateAsync(ur);
            return _mapper.Map<UserRoleDto>(ur);
        }
    }
}
