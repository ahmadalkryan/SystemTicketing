using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Notification
{
    public class CreateNotificationDto
    {
        public string Message { get; set; }

        public bool IsRead { get; set; }

        public DateTime SentAt { get; set; }

        public int TicketId { get; set; }

        public string UserID { get; set; }

    }
}
