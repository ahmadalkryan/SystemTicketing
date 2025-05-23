using Application.Dtos.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.TicketTraceDto
{
    public class UpdateTicketTraceDto:BaseDto<int>
    {
        public string? Note { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        = DateTime.Now;

        public int TicketId { get; set; }


        public int StatusID { get; set; }
        public string UserId { get; set; }
    }
}
