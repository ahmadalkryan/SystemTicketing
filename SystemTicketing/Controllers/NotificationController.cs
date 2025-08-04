using Application.Dtos.Action;
using Application.Dtos.common;
using Application.Dtos.Notification;
using Application.Dtos.TicketTraceDto;
using Application.IService;
using Application.Serializer;
using Infrastructure.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SystemTicketing.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IJsonFieldsSerializer _jsonFieldsSerializer;
        private readonly INotificationService _notificationService;

        public NotificationController(IJsonFieldsSerializer jsonFieldsSerializer,  INotificationService notificationService)
        {
            _jsonFieldsSerializer = jsonFieldsSerializer;
            _notificationService  = notificationService;
        }




        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<NotificationDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetAllNotifications()
        {
            var result = await _notificationService.GetAllNotifications();


            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "successfuly", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<NotificationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetNotifucatinById([FromQuery]BaseDto<int> dto)
        {
            var result = await _notificationService.GetNotificationByID(dto);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "successfuly", StatusCodes.Status200OK, result), string.Empty));

        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<NotificationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetNotificationsByUserId([FromQuery] string userId)
        {
            var result = await _notificationService.GetNotificationByUserId(userId);
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "successfuly", StatusCodes.Status200OK, result), string.Empty));

        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<NotificationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationDto createNotificationDto)
        {
            var result = await _notificationService.CreateNotification(createNotificationDto);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "successfuly", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse<NotificationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> DeleteNotification(BaseDto<int> dto)
        {
            var result = await _notificationService.DeleteNotifiaction(dto);

            return new RawJsonActionResult
                (_jsonFieldsSerializer.Serialize(new ApiResponse(true, "successfuly", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<NotificationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdateNotification([FromBody] UpdateNotificationDto updateNotificationDto)
        {
            var result = await _notificationService.UpdateNotification(updateNotificationDto);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "successfuly", StatusCodes.Status200OK, result), string.Empty));

        }


        //[HttpGet]
        //[ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]

        //public async Task<IActionResult> IsReadNotification([FromQuery] int id)
        //{
        //    var result = await _notificationService.IsReadNotification(id);

        //    return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "successfuly", StatusCodes.Status200OK, result), string.Empty));

        //}






















    }











}
