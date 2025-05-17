using DataAccessLyer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class DeviceCategory:BaseEntity
    {

        public DeviceCategory()
        {
            Tickets=new HashSet<Ticket>();
        }


        public string CategoryName { get; set; }
        public string Abbreviation { get; set; }
        public ICollection <Ticket>? Tickets{ get; set; }

    }
}

