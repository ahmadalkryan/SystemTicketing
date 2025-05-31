using Application.Dtos.Action;
using Application.Dtos.common;
using Application.Dtos.Notification;
using Application.Dtos.TicketTraceDto;
using Application.IService;
using Application.Serializer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SystemTicketing.Controllers
{
    [Route("api/[controller]")]
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


            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<TicketTraceDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetTicketTraceById(BaseDto<int> dto)
        {
            var result = await _ticketTraceService.GetTicketByID(dto);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<NotificationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNotification( CreateNotificationDto createNotificationDto)
        {
            var result = await _notificationService.CreateNotification(createNotificationDto);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse<NotificationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> DeleteNotification(BaseDto<int> dto)
        {
            var result = await _notificationService.DeleteNotifiaction(dto);

            return new RawJsonActionResult
                (_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<NotificationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdateNotification( UpdateNotificationDto updateNotificationDto)
        {
            var result = await _notificationService.UpdateNotification(updateNotificationDto);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]

        public async Task<IActionResult> IsReadNotification( int id)
        {
            var result = await _notificationService.IsReadNotification(id);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }






















    }











}
