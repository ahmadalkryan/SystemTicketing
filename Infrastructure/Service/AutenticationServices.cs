using Application.Dtos.LogIn;
using Application.IService;
using Application.LDAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class AutenticationServices
    {
        private readonly IUserService _userService;
       
        public AutenticationServices(IUserService userService)
        {
            _userService = userService;

        }
        public   List<LdapUser> _ldapUsers = new()
        {
            new LdapUser
        {
                ID ="1",
            Name = "emp1",
            Password = "emp1@123",
         //FullName = "محمد أحمد",
            Department = "قسم الموارد البشرية",
            Email = "m.ahmed@institute.edu",
           // Role = "Employee"
        },
            new LdapUser
        {
                ID ="2",
            Name = "emp3",
            Password = "emp1@123",
         //FullName = "محمد أحمد",
            Department = "قسم   IT ",
            Email = "m.ahmed@institute.edu",
           // Role = "Employee"
        },
        new LdapUser
        {
            ID ="3",
            Name = "maint1",
            Password = "maint@456",
          //  FullName = "علي محمود",
            Department = "قسم الصيانة",
            Email = "a.mahmoud@institute.edu",
         //   Role = "MaintenanceManager"
        },
         new LdapUser
        {
            ID ="4",
            Name = "maint1",
            Password = "maint@456",
          //  FullName = "علي محمود",
            Department = "قسم الصيانة",
            Email = "b.mahmoud@institute.edu",
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

           
           
                   LdapUser ldapUser= _ldapUsers.Where(x => x.Name == loginnDto.username && 
                   x.Password == loginnDto.password).FirstOrDefault()!;
           if(ldapUser == null)
            {
                throw new Exception();
            }
           else { return ldapUser; }
            

        }
    }
}
