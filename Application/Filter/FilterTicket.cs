using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Filter
{
    public class FilterTicket
    {
        public string? TicketNumber { get; set; }

        public DateTime? CreatedDate { get; set; }

        public TicketStatusEnum? _status { get; set; }
    }
}
