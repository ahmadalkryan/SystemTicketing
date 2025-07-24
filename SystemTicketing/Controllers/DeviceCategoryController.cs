using Application.Dtos.Action;
using Application.Dtos.common;
using Application.Dtos.DeviceCategory;
using Application.Dtos.Notification;
using Application.IService;
using Application.Serializer;
using Infrastructure.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SystemTicketing.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeviceCategoryController : ControllerBase
    {
        private readonly IDeviceCategoryService _deviceCategoryService;
        private readonly IJsonFieldsSerializer _jsonFieldsSerializer;

        public DeviceCategoryController(IDeviceCategoryService deviceCategoryService 
            ,IJsonFieldsSerializer jsonFieldsSerializer)
        {
            _deviceCategoryService = deviceCategoryService;
            _jsonFieldsSerializer = jsonFieldsSerializer;
        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<DeviceCategoryDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetAllDeviceCategory()
        {
            var result = await _deviceCategoryService.GetAllDeviceCategory();


            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<DeviceCategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetDeviceCategoryById([FromQuery] BaseDto<int> dto)
        {
            var result = await _deviceCategoryService.GetDeviceCategoryByID(dto);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }



        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<DeviceCategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateDeviceCategory([FromBody] CreateDeviceCategoryDto createDeviceCategoryDto )
        {
            var result = await _deviceCategoryService.CreateDeviceCategory(createDeviceCategoryDto);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }



        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse<DeviceCategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> DeleteDeviceCategory(BaseDto<int> dto)
        {
            var result = await _deviceCategoryService.DeleteDeviceCategory(dto);

            return new RawJsonActionResult
                (_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<DeviceCategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdateDeviceCategory([FromBody] UpdateDeviceCategoryDto updateDeviceCategoryDto)
        {
            var result = await _deviceCategoryService.UpdateDeviceCategory(updateDeviceCategoryDto);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }


    }
}
