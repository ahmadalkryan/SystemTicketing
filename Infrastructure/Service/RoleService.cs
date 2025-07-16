using Application.Dtos.Role;
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
    public class RoleService : IRoleService
    {
        private readonly  IAppRepository<Role> _appRepository;

        private readonly IMapper _mapper;

        public RoleService(IAppRepository<Role > appRepository ,IMapper mapper)
        {
            _appRepository = appRepository;
            _mapper = mapper;
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
    }
}
