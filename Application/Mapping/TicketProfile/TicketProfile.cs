using Application.Common;
using Application.Dtos.Ticket;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping.TicketProfile
{
    public class TicketProfile : Profile
    {

        public TicketProfile()
        {

            CreateMap<Ticket, TicketDto>();

            CreateMap<CreateTicketDto, Ticket>().ForMember(
                dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.UtcNow))
           .ForMember(
                des => des.TicketNumber, src => src.MapFrom<TicketNumberResolver>());
            CreateMap<updateTicketDto, Ticket>().ForMember(des => des.UpdatedDate, src => src.MapFrom(opt => DateTime.UtcNow))
                . ForMember(des => des.TicketNumber, s => s.MapFrom<TicketNumberResolver>());
        }
    }
}
