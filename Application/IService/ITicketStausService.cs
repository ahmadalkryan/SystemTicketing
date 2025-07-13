using Application.Dtos.common;
using Application.Dtos.Ticket;
using Application.Dtos.TicketStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface ITicketStausService
    {
        Task<IEnumerable<TicketStatusDto>> GetAllTicketStatus();
        Task<TicketStatusDto> GetTicketByID(BaseDto<int> dto);

        Task<TicketStatusDto> CreateTicketStatus(CreateTicketStatusDto createTicketStatusDto);

        Task<TicketStatusDto> UpdateTicketStatus(UpdateTicketStatusDto updateTicketStatusDto);

        Task<TicketStatusDto> DeleteTicketStatus(BaseDto<int> dto);
        
    }
}
