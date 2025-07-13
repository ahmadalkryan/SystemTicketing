using Application.Dtos.common;
using Application.Dtos.DeviceCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface IDeviceCategoryService
    {
        Task<IEnumerable<DeviceCategoryDto>> GetAllDeviceCategory();
        Task<DeviceCategoryDto> GetDeviceCategoryByID(BaseDto<int> dto);

        Task<DeviceCategoryDto> CreateDeviceCategory(CreateDeviceCategoryDto deviceCategory);
        Task<DeviceCategoryDto> UpdateDeviceCategory(UpdateDeviceCategoryDto deviceCategory);
        Task<DeviceCategoryDto> DeleteDeviceCategory(BaseDto<int> dto);
    }
}
