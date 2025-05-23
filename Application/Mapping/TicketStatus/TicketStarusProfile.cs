﻿using Application.Dtos.TicketStatus;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class TicketStarusProfile:Profile
    {
        public TicketStarusProfile()
        {
            CreateMap<TicketStatus, TicketStatusDto>();
            CreateMap<CreateTicketStatusDto, TicketStatus>();
            CreateMap<UpdateTicketStatusDto, TicketStatus>();
        }
    }
}
