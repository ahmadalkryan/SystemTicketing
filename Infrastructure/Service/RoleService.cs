using Application.Dtos.Role;
using Application.IRepository;
using Application.IService;
using AutoMapper;
using Domain.Entities;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class RoleService : IRoleService
    {
        private readonly  IAppRepository<Role> _appRepository;
        private readonly IAppRepository<UserRole> _userRoleRepository;
        private readonly IMapper _mapper;
        private readonly IUserRoleService _userRoleService;

        public RoleService(IAppRepository<Role > appRepository ,IMapper mapper ,IAppRepository<UserRole> appRepository1)
        {
            _appRepository = appRepository;
            _mapper = mapper;
            _userRoleRepository= appRepository1;
        }


        public async Task<RoleDto> CreateRole(CreateRoleDto createRoleDto)
        {
            var r = _mapper.Map<Role>(createRoleDto);
            await _appRepository.Insertasync(r);

            return _mapper.Map<RoleDto>(r);
        }

        public async Task<RoleDto> DeleteRole(string roleId)
        {
           var r =  (await _appRepository.FindAsync(x=>x.Id==roleId)).FirstOrDefault();
            await _appRepository.RemoveAsync(r);

            return _mapper.Map<RoleDto>(r);
        }

        public async Task<IEnumerable<RoleDto>> GetAllRoles() => _mapper.Map<IEnumerable<RoleDto>>(await _appRepository.GetAllAsync());
     

        public async Task<RoleDto> UpdateRole(UpdateRoleDto updateRoleDto)
        {
            var r = _mapper.
                Map<Role>(updateRoleDto);

            await _appRepository.Insertasync(r);
            return _mapper.Map<RoleDto>(r);
        }

        // get all role ;
      public async Task<IEnumerable<RoleDto>> GetRoleByUserId(string userId)
        {

            var userRoles = (await _userRoleRepository.GetAllAsync())
        .Where(x => x.UserId == userId)
        .ToList();

            var roleTasks = userRoles.Select(async ur =>
            {
                var role = await GetRoleById(ur.RoleId);
                return _mapper.Map<RoleDto>(role);
            });

            return await Task.WhenAll(roleTasks);

            
        }

        public async Task<RoleDto> GetRoleById(string roleId)
        {
            return _mapper.Map<RoleDto>(await _appRepository.GetById(roleId));
        }
    }
}
