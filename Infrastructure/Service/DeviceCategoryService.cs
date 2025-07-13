using Application.Dtos.common;
using Application.Dtos.DeviceCategory;
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
    public class DeviceCategoryService : IDeviceCategoryService
    {
        private readonly IAppRepository<DeviceCategory> _Repo;

        private readonly IMapper _mapper;


        public DeviceCategoryService(IAppRepository<DeviceCategory>appRepository ,IMapper mapper )
        {
            _Repo = appRepository;
            _mapper = mapper;
        }



        public async Task<DeviceCategoryDto> CreateDeviceCategory(CreateDeviceCategoryDto deviceCategory)
        {
           var d = _mapper.Map<DeviceCategory>( deviceCategory);

            await _Repo.Insertasync(d);
            return _mapper.Map<DeviceCategoryDto>(d);
        }

        public async Task<DeviceCategoryDto> DeleteDeviceCategory(BaseDto<int> dto)
        {
            var t = await _Repo.GetById(dto);

            await _Repo.RemoveAsync(t);
            return _mapper.Map<DeviceCategoryDto>(t);
        }

        public async Task<IEnumerable<DeviceCategoryDto>> GetAllDeviceCategory()
        {
          var d = await _Repo.GetAllAsync();
            return _mapper.Map<IEnumerable<DeviceCategoryDto>>(d);
        }

        public async Task<DeviceCategoryDto> GetDeviceCategoryByID(BaseDto<int> dto)
        {
            var d = await _Repo.GetById(dto);
            return _mapper.Map<DeviceCategoryDto>(d);
        }

        public async Task<DeviceCategoryDto>UpdateDeviceCategory(UpdateDeviceCategoryDto deviceCategory)
        {
            var d =  _mapper.Map<DeviceCategory>(deviceCategory);

           await _Repo.UpdateAsync(d);

            return _mapper.Map<DeviceCategoryDto>(d);
        }
    }
}
