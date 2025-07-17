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
            Name = "emp1",
            Password = "emp1@123",
         //FullName = "محمد أحمد",
            Department = "قسم الموارد البشرية",
            Email = "m.ahmed@institute.edu",
           // Role = "Employee"
        },
            new LdapUser
        {
            Name = "emp3",
            Password = "emp1@123",
         //FullName = "محمد أحمد",
            Department = "قسم   IT ",
            Email = "m.ahmed@institute.edu",
           // Role = "Employee"
        },
        new LdapUser
        {
            Name = "maint1",
            Password = "maint@456",
          //  FullName = "علي محمود",
            Department = "قسم الصيانة",
            Email = "a.mahmoud@institute.edu",
         //   Role = "MaintenanceManager"
        }
        };

        public  bool Authenticate(LoginDto loginnDto)
        {
            var ldap = _ldapUsers.FirstOrDefault(x => x.Name == loginnDto.username && x.Password == loginnDto.password);

            if (ldap == null)
            {
                return false;

            }




            return true;



        }
        public LdapUser GEtUser(LoginDto loginnDto)
        {

           
           
                   LdapUser ldapUser= _ldapUsers.FirstOrDefault(x => x.Name == loginnDto.username && 
                   x.Password == loginnDto.password)!;
           if(ldapUser == null)
            {
                throw new Exception();
            }
           else { return ldapUser; }
            

        }
    }
}
