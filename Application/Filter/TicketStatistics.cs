using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Filter
{
    public  class TicketStatistics
    {
        public int TotalTickets { get; set; }

        public int PendingTickets { get; set; }

        public int CompleteTickets { get; set; }

        public int refundTickets { get; set; }

        public int NewTickets { get; set; }



    }
}
