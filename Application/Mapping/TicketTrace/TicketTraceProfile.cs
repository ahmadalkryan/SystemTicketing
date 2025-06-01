using Application.Dtos.TicketTraceDto;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class TicketTraceProfile:Profile
    {
        public TicketTraceProfile()
        {
            CreateMap<TicketTrace,TicketTraceDto>();
            CreateMap<CreateTicketTraceDto,TicketTrace>().ForMember(des =>des.CreateTime,des=>des.MapFrom(des=>DateTime.UtcNow));
            CreateMap<UpdateTicketTraceDto,TicketTrace>().ForMember(des => des.UpdateTime , src=>src.MapFrom(des=>DateTime.UtcNow));
        }
    }
}
