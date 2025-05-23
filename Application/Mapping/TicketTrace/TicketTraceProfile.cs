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
            CreateMap<CreateTicketTraceDto,TicketTrace>();
            CreateMap<UpdateTicketTraceDto,TicketTrace>();
        }
    }
}
