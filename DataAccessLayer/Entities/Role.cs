using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLyer.Entities.UserEntities
{
    public  class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }
        public string Id { get; set; }
        public string Name { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
