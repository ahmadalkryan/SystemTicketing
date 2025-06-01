using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
   public  class User
    {
        public User()
        {
             UserRoles = new HashSet<UserRole>();
            Notifications = new HashSet<Notification>();
            TicketTraces = new HashSet<TicketTrace>();
        }
        public string UserId { get; set; }

        public string Name { get; set; }

        public string Department {   get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public ICollection<Notification> Notifications { get; set; }
        

        public ICollection<TicketTrace> TicketTraces { get; set; }


        public ICollection<UserRole>UserRoles { get; set; }
    }
}
