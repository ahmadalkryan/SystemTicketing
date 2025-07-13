using DataAccessLyer.Common;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class TicketTrace:BaseEntity
    {
      //  public string ActionType { get; set; }

        public string Note { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        = DateTime.Now;

        public int TicketId { get; set; }

        public Ticket? _ticket {  get; set; }

        public string UserId { get; set; }

        public User? _user { get; set; }

        public TicketStatus? _ticketStatus { get; set; }

        public int StatusID { get; set; }
    }
}
