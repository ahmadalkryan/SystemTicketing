using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Ticket
{
    public class CreateTicketDto
    {

        
        public string Description { get; set; }

     

        public int DeciveCategoryId { get; set; }


        public IFormFile? ImageFile { get; set; }
        



    }
}

//public int TicketStatusId { get; set; }
//public string TicketNumber { get; set; }
//   public string AttachmentPath { get; set; }