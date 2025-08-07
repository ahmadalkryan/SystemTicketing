using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.LogIn
{
    
        public class LdapUserDto
        {
            public string UserID { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; } 
            public string Department { get; set; }
            public string Dn { get; set; }
            // يمكنك إضافة خصائص أخرى حسب بنية LDAP الخاصة بك
        }
    
}
