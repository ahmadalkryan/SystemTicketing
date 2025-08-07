using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.LDAP
{
    public class LdapSettings
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string BaseDn { get; set; }
        public string BindDn { get; set; }
        public string BindPassword { get; set; }
        public string SearchFilter { get; set; }
    }

}
