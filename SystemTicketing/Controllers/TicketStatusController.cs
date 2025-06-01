using Application.Dtos.Action;
using Application.Dtos.common;
using Application.Dtos.Ticket;
using Application.Dtos.TicketStatus;
using Application.IService;
using Application.Serializer;
using Infrastructure.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SystemTicketing.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TicketStatusController : ControllerBase
    {
        private readonly IJsonFieldsSerializer _jsonFieldsSerializer;

        private readonly ITicketStausService _ticketStausService;

        public TicketStatusController(ITicketStausService ticketStausService, IJsonFieldsSerializer jsonFieldsSerializer)
        {
             _jsonFieldsSerializer = jsonFieldsSerializer;
            _ticketStausService = ticketStausService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<TicketStatusDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetAllTicketStatus()
        {
            var result = await _ticketStausService.GetAllTicketStatus();

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<TicketStatusDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]



        public async Task<IActionResult> GetTicketStatusById([FromQuery]BaseDto<int> dto)
        {
            var result = await _ticketStausService.GetTicketByID(dto);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<TicketStatusDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> InsertTicket( [FromBody]CreateTicketStatusDto createTicketStatusDto)
        {
            var result = await _ticketStausService.CreateTicketStatus(createTicketStatusDto);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }


        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse<TicketStatusDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> DeleteTicketStatus(BaseDto<int> dto)
        {
            var result = await _ticketStausService
                .DeleteTicketStatus(dto);

            return new RawJsonActionResult
                (_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<TicketStatusDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdateTicketStatus([FromBody]UpdateTicketStatusDto updateTicketStatusDto )
        {
            var result = await _ticketStausService .UpdateTicketStatus(updateTicketStatusDto);  

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));

        }









    }
}
