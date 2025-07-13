using Application.Dtos.common;
using Application.Dtos.TicketTraceDto;
using Application.IRepository;
using Application.IService;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class TicketTraceService : ITicketTraceService
    {

        private readonly IAppRepository<TicketTrace> _repo;
        private readonly IMapper _mapper;
        private readonly ITicketService _service;


        public TicketTraceService(IAppRepository <TicketTrace> T ,IMapper mapper)
        {
             _repo = T;
            _mapper = mapper;
        }


        public async Task<TicketTraceDto> CreateTicket(CreateTicketTraceDto createTicketTraceDto)
        {
           var t = _mapper.Map<TicketTrace>(createTicketTraceDto);
            await _repo.Insertasync(t);

            return _mapper.Map<TicketTraceDto>(t);
        }

        public async Task<TicketTraceDto> DeleteTicket(BaseDto<int> dto)
        {
         
            var t =  await _repo.GetById(dto.Id);

            await _repo.RemoveAsync(t);

            return _mapper.Map<TicketTraceDto>(t);
        }

        public async Task<IEnumerable<TicketTraceDto>> GetAllTickets() => _mapper.Map<IEnumerable<TicketTraceDto>>(await _repo.GetAllAsync());
       

        public async Task<TicketTraceDto> GetTicketByID(BaseDto<int> dto)
        {
            return _mapper.Map<TicketTraceDto>( await _repo.GetById(dto.Id));
        }

        public async Task<TicketTraceDto> UpdateTicket(UpdateTicketTraceDto updateTicketTraceDto)
        {
            var t = _mapper.Map<TicketTrace>(updateTicketTraceDto);

            await _repo.UpdateAsync(t);
            return _mapper.Map<TicketTraceDto>(t);

        }

        public async Task<IEnumerable<TicketTraceDto>> GetTicketTracesForTicket(int ticketId)
        {
           var t = await _repo.FindAsync(x=>x.TicketId == ticketId,x=>x._ticket! );
            return _mapper.Map<IEnumerable<TicketTraceDto>>(t);
        }

        public async Task<IEnumerable<TicketTraceDto>> GetTicketTracesForUser(string userId)
        {
            var  t= await _repo.FindAsync(x=>x.UserId ==userId ,x=>x._user!);


            return _mapper.Map<IEnumerable<TicketTraceDto>>(t);

        }

        public async Task<IEnumerable<TicketTraceDto>> GetTicketTraceForTicketByNumber(string ticketnumber)
        {
            var ticket = await _service.GetTicketByNumber(ticketnumber);

            var tickettrace = await _repo.FindAsync(x=>x.TicketId==ticket.Id,x=>x._ticket! );

            return _mapper.Map<IEnumerable<TicketTraceDto>>(tickettrace);

        }
    }
}
