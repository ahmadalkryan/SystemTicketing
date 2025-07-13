using Application.Dtos.DeviceCategory;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class DeviceCategoryProfile:Profile
    {
        public DeviceCategoryProfile()
        {
            CreateMap<DeviceCategory,DeviceCategoryDto>();
            CreateMap<CreateDeviceCategoryDto, DeviceCategory>();
            CreateMap<UpdateDeviceCategoryDto, DeviceCategory>();

        }
    }
}
