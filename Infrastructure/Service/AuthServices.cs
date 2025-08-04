using Application.Dtos.LogIn;
using Application.IService;
using Application.LDAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class AuthServices
    {
        private readonly IUserService _userService;
       
        public AuthServices(IUserService userService)
        {
            _userService = userService;

        }
        public   List<LdapUser> _ldapUsers = new()
        {
            new LdapUser
        {
                UserID ="5",
            Name = "emp1",
            Password = "ahmad2004@",
         //FullName = "محمد أحمد",
            Department = "قسم الموارد البشرية",
            Email = "m.ahmed@institute.edu",
           // Role = "Employee"
        },
            new LdapUser
        {
                UserID ="6",
            Name = "ali",
            Password = "ahmad2004#",
         //FullName = "محمد أحمد",
            Department = "قسم   IT ",
            Email = "m.ahmed@institute.edu",
           // Role = "Employee"
        },
        new LdapUser
        {
            UserID ="7",
            Name = "",
            Password = "maint@456",
          //  FullName = "علي محمود",
            Department = "قسم الصيانة",
            Email = "a.mahmoud@institute.edu",
         //   Role = "MaintenanceManager"
        },
         new LdapUser
        {
           UserID ="4",
            Name = "ahmad",
            Password = "ahmad2004#",
          //  FullName = "علي محمود",
            Department = "IT",
            Email = "ahmad.alkryan@hiast.edu.sy",
         //   Role = "MaintenanceManager"
        }
        };

        public  bool Authenticate(LoginDto loginDto)
        {

            return _ldapUsers.Any(x =>
            x.Name == loginDto.Name &&
            x.Password == loginDto.Password);
        }
            //var ldap = _ldapUsers.FirstOrDefault(x => x.Name == loginnDto.username && x.Password == loginnDto.password);

            //if (ldap == null)
            //{
            //    return false;

            //}




            //return true;



        
        public LdapUser GetUser(LoginDto loginDto)
        {

            var user = _ldapUsers.FirstOrDefault(x =>
             x.Name == loginDto.Name &&
             x.Password == loginDto.Password);

            return user ?? throw new Exception("User not found");

            //        LdapUser ldapUser= _ldapUsers.Where(x => x.Name == loginnDto.username && 
            //        x.Password == loginnDto.password).FirstOrDefault()!;
            //if(ldapUser == null)
            // {
            //     throw new Exception();
            // }
            //else { return ldapUser; }


        }
    }
}
