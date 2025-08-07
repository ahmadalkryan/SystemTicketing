using Application.Dtos.LogIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IService
{
    public interface ILdapAuthenticationService
    {
       
        
            bool Authenticate(string username, string password, out string userDn, out Dictionary<string, List<string>> attributes);
            LdapUserDto GetUser(string username);
        
    }
}
