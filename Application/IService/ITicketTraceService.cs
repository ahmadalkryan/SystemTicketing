using Application.Dtos.common;
using Application.Dtos.Ticket;
using Application.Dtos.TicketTraceDto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface ITicketTraceService
    {
        Task<IEnumerable<TicketTraceDto>> GetAllTickets();
        Task<TicketTraceDto> GetTicketByID(BaseDto<int> dto);

        Task<TicketTraceDto> CreateTicket(CreateTicketTraceDto createTicketTraceDto );

        Task<TicketTraceDto> UpdateTicket(UpdateTicketTraceDto updateTicketTraceDto );

        Task<TicketTraceDto> DeleteTicket(BaseDto<int> dto);

        Task<IEnumerable<TicketTraceDto>> GetTicketTracesForTicket(int ticketId);

        Task<IEnumerable<TicketTraceDto>> GetTicketTracesForUser(string userId);



    }
}
