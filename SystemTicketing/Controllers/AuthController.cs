using Application.Dtos.LogIn;
using Application.IService;
using Application.LDAP;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SystemTicketing.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AutenticationServices _authenticationService;

        public AuthController(IUserService userService , AutenticationServices authenticationService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
        }


        [HttpPost]
        public  async Task<IActionResult> Login([FromBody]LoginDto loginDto)
        {

            //LdapUser user = AutenticationService._ldapUsers
            //     .FirstOrDefault(x => x.Username == loginDto.username && x.Password == loginDto.password);

            var Isldap = _authenticationService.Authenticate(loginDto);



            if (Isldap == false )
            
            {

                return Unauthorized(" Unauthorized user ");

            }

            // get user from Ldap 
            var user = _authenticationService.GEtUser(loginDto);
            // exists in Db 
            var existuser = _userService.GetUserByEmail(user.
                    Email);

            if(existuser == null)
            {
               
               await _userService.InsertUser(user);

            }
            return Ok(loginDto);


        }












    }
}
