using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.LDAP
{
    public class LdapUser
    {

        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } // "Employee" أو "MaintenanceManager" "MaintenanceEmployee" 
    }
}
