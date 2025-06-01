using Application.Dtos.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Ticket
{
    public class updateTicketDto:BaseDto<int>
    {

       
      
        public string Description { get; set; }

        public string AttachmentPath { get; set; }
        public int TicketStatusId { get; set; }

        public int DeciveCategoryId { get; set; }
      //  public DateTime CreatedDate { get; set; }
       // public DateTime UpdatedDate { get; set; }


    }
}
