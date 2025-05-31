using Application.Dtos.common;
using Application.Dtos.Ticket;
using Application.Dtos.TicketStatus;
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
    public class TicketStuatusService : ITicketStausService
    {
          private readonly IAppRepository<TicketStatus> _repo ;

          private readonly IMapper _mapper ;


        public TicketStuatusService(IAppRepository<TicketStatus> appRepository ,IMapper mapper)
        {
             _repo = appRepository ;
            _mapper = mapper ;
        }


        public async Task<TicketStatusDto> CreateTicketStatus(CreateTicketStatusDto createTicketStatusDto)
        {
             var t = _mapper.Map<TicketStatus>(createTicketStatusDto);
            await _repo.Insertasync(t);
            return _mapper.Map<TicketStatusDto>(t);
        }

        public async Task<TicketStatusDto> DeleteTicketStatus(BaseDto<int> dto)
        {
           var t = await _repo.GetById(dto.Id);

            await _repo.RemoveAsync(t);

            return _mapper.Map<TicketStatusDto>(t) ;

        }

        public async Task<IEnumerable<TicketStatusDto>> GetAllTicketStatus()
        {
            return _mapper.Map<IEnumerable<TicketStatusDto>>(await _repo.GetAllAsync());
        }

        public async Task<TicketStatusDto> GetTicketByID(BaseDto<int> dto)
        {
           return _mapper.Map<TicketStatusDto>(await _repo.GetById(dto.Id));
        }

        public async Task<TicketStatusDto> UpdateTicketStatus(UpdateTicketStatusDto updateTicketStatusDto)
        {
            var t = _mapper.Map<TicketStatus>(updateTicketStatusDto);
            await _repo.UpdateAsync(t);
            return _mapper.Map<TicketStatusDto>(t);
        }

       
    }
}
