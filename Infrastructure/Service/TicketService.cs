using Application.Dtos.common;
using Application.Dtos.Ticket;
using Application.IRepository;
using Application.IService;
using AutoMapper;
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

         var T =  _mapper.Map<Ticket>(t);

            await  _repo.RemoveAsync(t);
            return _mapper.Map<TicketDto>(T);

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
    }
}
