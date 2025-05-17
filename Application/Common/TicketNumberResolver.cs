using Application.Dtos.Ticket;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class TicketNumberResolver : IValueResolver<CreateTicketDto, Ticket, string>
    {
        private readonly ITicketNumberGenerator _generator;

        public TicketNumberResolver(ITicketNumberGenerator generator)
        {
            _generator = generator;
        }

        public string Resolve(CreateTicketDto source, Ticket destination, string destMember, ResolutionContext context)
        {
            return _generator.GenerateTicketNumber(source.DeciveCategoryId);
        }
    }
}
