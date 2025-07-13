using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.DeviceCategory
{
    public class CreateDeviceCategoryDto
    {
        public string CategoryName { get; set; }
        public string Abbreviation { get; set; } = string.Empty;
    }
}
