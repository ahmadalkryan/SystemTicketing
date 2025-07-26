using Application.Dtos.Action;
using Application.Dtos.common;
using Application.Dtos.Ticket;
using Application.Filter;
using Application.IService;
using Application.Serializer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SystemTicketing.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TicketController : ControllerBase

    {  
         private readonly ITicketService _ticketService;
         private readonly IJsonFieldsSerializer _jsonFieldsSerializer;

        public TicketController( ITicketService ticketService ,IJsonFieldsSerializer jsonFieldsSerializer )
        {
             _jsonFieldsSerializer = jsonFieldsSerializer;
            _ticketService = ticketService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<TicketDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetAllTickets()
        {
            var result = await _ticketService.GetAllTickets();

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK,result), string.Empty));
        }



        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<TicketDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

         public async Task<IActionResult> GetTicketById([FromQuery] BaseDto<int> dto)
        {
            var result = await _ticketService.GetTicketByID(dto);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true ,"", StatusCodes.Status200OK ,result), string.Empty));

        }



        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<TicketDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> GetTicketByNumber([FromQuery] string number)
        {
            var result = await _ticketService.GetTicketByNumber(number);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<TicketDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
       [ServiceFilter(typeof(TicketNotificationFilter))]
        public async Task<IActionResult > InsertTicket([FromBody] CreateTicketDto dto)
        {
            var result = await _ticketService.CreateTicket(dto);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse<TicketDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> DeleteTicket(BaseDto<int> dto)
        {
            var result = await _ticketService.DeleteTicket(dto);

            return new RawJsonActionResult
                (_jsonFieldsSerializer.Serialize(new ApiResponse(true , "" ,StatusCodes.Status200OK, result),string.Empty));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<TicketDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdateTicket([FromBody]updateTicketDto updateTicketDto)
        {
            var result = await _ticketService.UpdateTicket(updateTicketDto);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<TicketDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> FilterTicket([FromQuery] FilterTicket filterTicket)
        {
            var result =await _ticketService.FilterTicket(filterTicket);

            return new RawJsonActionResult
               (_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<TicketStatistics>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> TicketStatistics( )
        {
            var result = await _ticketService.TicketStatistic();

            return new RawJsonActionResult
               (_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<TicketDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
       
        public async Task<IActionResult> FilterTicketByDate([FromQuery] FilterDate filterDate)
        {
            var result = await _ticketService.FilterTicketByDate(filterDate);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }







    }
}
