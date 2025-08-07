using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.LDAP
{
    public class AuthResponseDto
    {
        public class LdapUserDto
        {
            public string UserID { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; } // لا يتم تخزينها فعلياً، فقط للإشارة
            public string Department { get; set; }
            public string Dn { get; set; }
            // يمكنك إضافة خصائص أخرى حسب بنية LDAP الخاصة بك
        }
        [Required]
            public string Token { get; set; }
        [Required]
        public string userId { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Department { get; set; }

        [Required ]

        public string role { get; set; }
        
    }
}
