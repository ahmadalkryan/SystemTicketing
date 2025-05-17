using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class UserRole
    {

        public string UserId {  get; set; }
        public string RoleId { get; set; }

        public User _user { get; set; }

        public Role _role { get; set; }
    }
}
