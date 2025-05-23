using Application.Dtos.common;
using Application.Dtos.TicketTraceDto;
using Application.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class TicketTraceService : ITicketTraceService
    {
        public Task<TicketTraceDto> CreateTicket(CreateTicketTraceDto createTicketTraceDto)
        {
            throw new NotImplementedException();
        }

        public Task<TicketTraceDto> DeleteTicket(BaseDto<int> dto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TicketTraceDto>> GetAllTickets()
        {
            throw new NotImplementedException();
        }

        public Task<TicketTraceDto> GetTicketByID(BaseDto<int> dto)
        {
            throw new NotImplementedException();
        }

        public Task<TicketTraceDto> UpdateTicket(UpdateTicketTraceDto updateTicketTraceDto)
        {
            throw new NotImplementedException();
        }
    }
}
