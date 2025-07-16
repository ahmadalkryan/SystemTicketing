using Application.Dtos.LogIn;
using Application.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.LDAP
{
    public class AutenticationServices
    {
        private readonly IUserService _userService;
        public AutenticationServices(IUserService userService)
        {
            _userService = userService;

        }
        public readonly  List<LdapUser> _ldapUsers = new()
        {
            new LdapUser
        {
            Username = "emp1",
            Password = "emp1@123",
            FullName = "محمد أحمد",
            Department = "قسم الموارد البشرية",
            Email = "m.ahmed@institute.edu",
            Role = "Employee"
        },
        new LdapUser
        {
            Username = "maint1",
            Password = "maint@456",
            FullName = "علي محمود",
            Department = "قسم الصيانة",
            Email = "a.mahmoud@institute.edu",
            Role = "MaintenanceManager"
        }
        };

        public  bool Authenticate(LoginDto loginnDto)
        {
            var ldap = _ldapUsers.FirstOrDefault(x => x.Username == loginnDto.username && x.Password == loginnDto.password);

            if (ldap == null)
            {
                return false;

            }




            return true;



        }
        public LdapUser GEtUser(LoginDto loginnDto)
        {
            return _ldapUsers.FirstOrDefault(x => x.Username == loginnDto.username && x.Password == loginnDto.password);

        }
    }
}
