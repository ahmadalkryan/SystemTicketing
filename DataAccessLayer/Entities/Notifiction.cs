using DataAccessLyer.Common;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Notifiction:BaseEntity
    {
         public string Message { get; set; }

        public bool IsRead  { get; set; }

        public DateTime SentAt { get; set; }

        public int TicketId     { get; set; }

        public string UserID { get; set; }

        public User _user {  get; set; }

        public Ticket _ticket { get; set; }
    }
}
