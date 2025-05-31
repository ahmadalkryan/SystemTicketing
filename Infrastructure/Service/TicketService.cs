using Application.Dtos.common;
using Application.Dtos.Ticket;
using Application.Filter;
using Application.IRepository;
using Application.IService;
using AutoMapper;
using DataAccessLyer.Enum;
using Domain.Entities;
using Infrastructure.Repository;
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

             if( filterTicket.TicketNumber != null)
            {
                query = query.Where(x=>x.TicketNumber == filterTicket.TicketNumber);
            }
             if(filterTicket.CreatedDate != null)
            {
                query = query.Where(x => x.CreatedDate == filterTicket.CreatedDate);
            }

             if(filterTicket._status!= null)
            {
                query=query.Where(x=>x._status.StatusName==filterTicket._status);
            }
            return _mapper.Map<IEnumerable<TicketDto>>(query);
        }

        public async Task<TicketStatistics> TicketStatistic()
        {
            var t = await _repo.GetAllAsync();

            TicketStatusEnum Tc = TicketStatusEnum.Complete;
            TicketStatusEnum Tp = TicketStatusEnum.Pending;
            TicketStatusEnum Tr = TicketStatusEnum.Refund;


            TicketStatistics ts = new TicketStatistics()
            {

                TotalTickets = t.Count(),
                CompleteTickets = t.Where(x=>x._status.StatusName.ToString()==Tc.ToString()).Count(),
                PendingTickets = t.Where(t => t._status.StatusName == TicketStatusEnum.Pending).Count(),
                refundTickets = t.Where(t =>t._status.StatusName.ToString().Equals(Tc.ToString())).Count()
            };

            

             return ts;

        }

        public  async Task<IEnumerable<TicketDto>> FilterTicketByDate(FilterDate filterDate)
        {
            var query = await _repo.GetAllAsync();

            if(filterDate.startDate!= null&& filterDate.endDate !=null)
            {
                query = query.Where(x=>x.CreatedDate>=filterDate.startDate&& x.CreatedDate<=filterDate.endDate);
            }

            return _mapper.Map<IEnumerable<TicketDto>>(query);
        }

    }
}
