using DataAccessLyer.Common;
using DataAccessLyer.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLyer.Entities.AppEntities
{
   public class TicketStatus:BaseEntity
    {
        public TicketStatus()
        {
            Tickets =new HashSet<Ticket>();
        }
        public TicketStatusEnum StatusName { get; set; }
        public  ICollection<Ticket>?Tickets { get; set; }

        public ICollection<TicketTrace>? TicketsTraces { get; set; }

    }
}
