using Application.Dtos.Action;
using Application.Dtos.common;
using Application.Dtos.Ticket;
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
    public class TicketTraceController : ControllerBase
    {
        private readonly IJsonFieldsSerializer _jsonFieldsSerializer;
        private readonly ITicketTraceService _ticketTraceService;

        public TicketTraceController(IJsonFieldsSerializer jsonFieldsSerializer ,ITicketTraceService ticketTraceService)
        {
             _jsonFieldsSerializer = jsonFieldsSerializer;
            _ticketTraceService = ticketTraceService;
        }




        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<TicketTraceDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetAllTicketTrace()
        {
            var result = await _ticketTraceService.GetAllTickets();


            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, " successfuly", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<TicketTraceDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetTicketTraceById([FromQuery]BaseDto<int> dto)
        {
            var result = await _ticketTraceService.GetTicketByID(dto);
                
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "successfuly", StatusCodes.Status200OK, result), string.Empty));

        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<TicketTraceDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetTicketTraceByNumber([FromQuery] string TicketNumber)
        {
            var result = await _ticketTraceService.GetTicketTraceForTicketByNumber(TicketNumber);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "successfuly", StatusCodes.Status200OK, result), string.Empty));

        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<TicketTraceDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ServiceFilter(typeof(TicketNotificationFilter))]
        public async Task<IActionResult> InsertTicketTrace([FromBody]CreateTicketTraceDto createTicketTraceDto)
        {
            var result = await _ticketTraceService.CreateTicket(createTicketTraceDto);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "successfuly", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse<TicketTraceDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> DeleteTicketTrace(BaseDto<int> dto)
        {
            var result = await _ticketTraceService.DeleteTicket(dto);

            return new RawJsonActionResult
                (_jsonFieldsSerializer.Serialize(new ApiResponse(true , "" ,StatusCodes.Status200OK, result),string.Empty));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<TicketTraceDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdateTicketTrace([FromBody] UpdateTicketTraceDto updateTicketTraceDto)
        {
            var result = await _ticketTraceService.UpdateTicket(updateTicketTraceDto);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "successfuly", StatusCodes.Status200OK, result), string.Empty));

        }



        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<TicketTraceDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
       

        public async Task<IActionResult> GetAllTicketTraceForTicket([FromQuery] int id)
        {
            var result = await _ticketTraceService.GetTicketTracesForTicket(id);


            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "successfuly", StatusCodes.Status200OK, result), string.Empty));
        }
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<TicketTraceDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]


        public async Task<IActionResult> GetAllTicketTraceForUser([FromQuery] string UserId)
        {
            var result = await _ticketTraceService.GetTicketTracesForUser(UserId);


            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "successfuly", StatusCodes.Status200OK, result), string.Empty));
        }


    }
}
