using Application.Dtos.common;
using Application.Dtos.Ticket;
using Application.Filter;
using Application.IRepository;
using Application.IService;
using AutoMapper;
using Domain.Entities;
using Domain.Enum;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class TicketService : ITicketService
    {
        private readonly IAppRepository<Ticket> _repo;
        private readonly IMapper _mapper;
        public TicketService(IAppRepository<Ticket> appRepository ,IMapper mapper)
        {
             _repo = appRepository;
            _mapper = mapper;
        }
        public async Task<TicketDto> CreateTicket(CreateTicketDto createTicketDto)
        {
            await _repo.Insertasync(_mapper.Map<Ticket>(createTicketDto));

            var t = _mapper.Map<Ticket>(createTicketDto);
            return _mapper.Map<TicketDto>(t);


        }

        public async Task<TicketDto> DeleteTicket(BaseDto<int> dto)
        {
           var t = (await _repo.FindAsync(x => x.Id == dto.Id)).FirstOrDefault();

        

            await  _repo.RemoveAsync(t);
            return _mapper.Map<TicketDto>(t);

        }

        public async Task<IEnumerable<TicketDto>> GetAllTickets() => _mapper.Map<IEnumerable<TicketDto>>(await _repo.GetAllAsync());
        

        public async Task<TicketDto> GetTicketByID(BaseDto<int> dto)
        {
            var t = await _repo.GetById(dto.Id);

            return _mapper.Map<TicketDto>(t);
        }

        public async Task<TicketDto> UpdateTicket(updateTicketDto updateTicketDto)
        {
           var t = _mapper.Map<Ticket>(updateTicketDto);

            await _repo.UpdateAsync(t);

            return _mapper.Map<TicketDto>(t);
        }

      public async Task<IEnumerable<TicketDto>>  FilterTicket(FilterTicket filterTicket)
        {
            var query = await _repo.GetAllAsync();

            

            if (!string.IsNullOrEmpty(filterTicket.TicketNumber))
            {
                query = query.Where(x => x.TicketNumber == filterTicket.TicketNumber);
            }

            if (filterTicket.CreatedDate.HasValue)
            {
               
                var startDate = filterTicket.CreatedDate.Value.Date;
                var endDate = startDate.AddDays(1).AddTicks(-1);

                query = query.Where(x => x.CreatedDate >= startDate && x.CreatedDate <= endDate);
            }

            if (filterTicket.DeciveCategoryId!=null)
            {

                query = query.Where(x => x.DeciveCategoryId == filterTicket.DeciveCategoryId);
                  
            }
            return _mapper.Map<IEnumerable<TicketDto>>(query);
        }

        public async Task<TicketStatistics> TicketStatistic()
        {
            var tickets = await _repo.GetAllAsync();
            


            TicketStatistics ticketStatistics = new TicketStatistics()
            {
                TotalTickets = tickets.Count(),
                CompleteTickets = tickets.Count(x => x.TicketStatusId == 4),



                PendingTickets = tickets.Count(x =>x.TicketStatusId == 1),
                    

                refundTickets = tickets.Count(x =>x.TicketStatusId==3),

                NewTickets = tickets.Count(x=>x.TicketStatusId==2), 
                   


            };
        

             return ticketStatistics;

        }

        public  async Task<IEnumerable<TicketDto>> FilterTicketByDate([FromBody]FilterDate filterDate)
        {
            var query = await _repo.GetAllAsync();

            if(filterDate.startDate!= default && filterDate.endDate !=default)
            {
                query = query.Where(x=>x.CreatedDate>=filterDate.startDate&& x.CreatedDate<=filterDate.endDate);
            }

            return _mapper.Map<IEnumerable<TicketDto>>(query);
        }

        public async Task<TicketDto> GetTicketByNumber(string TicketNumber)
        {
           var tickets = await _repo.GetAllAsync();

            var t = tickets.Where(x => x.TicketNumber == TicketNumber).FirstOrDefault();

            return _mapper.Map<TicketDto>(t);

        }
    }
}
