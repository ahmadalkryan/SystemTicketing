using Application.Dtos.common;
using Application.Dtos.Ticket;
using Application.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
   public interface ITicketService
    {
        Task<IEnumerable<TicketDto>>  GetAllTickets();
        Task<TicketDto>  GetTicketByID(BaseDto<int> dto);

        Task<TicketDto> CreateTicket(CreateTicketDto createTicketDto);

        Task<TicketDto> UpdateTicket(updateTicketDto updateTicketDto);

        Task<TicketDto> DeleteTicket(BaseDto<int> dto);

        Task<IEnumerable<TicketDto>> FilterTicket(FilterTicket filterTicket);

        Task<TicketStatistics> TicketStatistic();

        Task<IEnumerable<TicketDto>> FilterTicketByDate(FilterDate filterDate);


    }
}
