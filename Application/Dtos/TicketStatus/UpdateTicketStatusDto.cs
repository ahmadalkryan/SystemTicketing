using Application.Dtos.common;
using DataAccessLyer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.TicketStatus
{
    public class UpdateTicketStatusDto:BaseDto<int>
    {
        public TicketStatusEnum StatusName { get; set; }
    }
}
