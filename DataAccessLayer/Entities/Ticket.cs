using DataAccessLyer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLyer.Entities.AppEntities
{
    public class Ticket:BaseEntity
    {
        public Ticket()
        {
            Notifictions =new HashSet<Notifiction>();
            ticketTraces = new HashSet<TicketTrace>();
        }
        public  string TicketNumber { get; set; }
        public string Description { get; set; }

        public string AttachmentPath {  get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public TicketStatus? _status { get; set; }
        public ICollection<Notifiction> Notifictions { get; set; }
       public int TicketStatusId { get; set; }

        public int DeciveCategoryId { get; set; }

        public DeviceCategory _deviceType { get; set; }

        public ICollection<TicketTrace> ticketTraces { get; set; }




    }
}
