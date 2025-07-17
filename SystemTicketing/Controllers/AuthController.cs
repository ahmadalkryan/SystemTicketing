using Application.Dtos.LogIn;
using Application.IService;
using Application.LDAP;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Service;
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
        private readonly IMapper _mapper;
        private readonly TokenService _tokenSevice ;

        public AuthController(IUserService userService , AutenticationServices authenticationService,IMapper mapper ,TokenService tokenSevice)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
            _userService = userService;
            _tokenSevice = tokenSevice;
        }


        [HttpPost]
        public  async Task<IActionResult> Login([FromBody]LoginDto loginDto)
        {

            //LdapUser user = AutenticationService._ldapUsers
            //     .FirstOrDefault(x => x.Username == loginDto.username && x.Password == loginDto.password);

            var Isldap = _authenticationService.Authenticate(loginDto);



            if (!Isldap )
            
            {

                return Unauthorized(" Unauthorized user ");

            }
            
                // get user from Ldap 
                var ldapUser = _authenticationService.GEtUser(loginDto);
            if(ldapUser == null){
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            // exists in Db 
            var existuser = _userService.GetUserByEmail(ldapUser.
                    Email);

            if (existuser == null)
            {
               
              await _userService.InsertUser(ldapUser);
                
               
               
            }
            var user = await _userService.GetUserByEmail(ldapUser.Email);
            User newUser = new User
            {
                UserId = user.UserId,
                Name = ldapUser.Name,
                Email = ldapUser.Email,
                Password = ldapUser.Password,
                Department = ldapUser.Department,
            };
            var token = _tokenSevice.GenerateToken(newUser);

            return Ok(new AuthResponseDto
            {
                Token = token,
                UserId = newUser.UserId,
                FullName =newUser.Name,
                Email = newUser.Email,
                Department = newUser.Department
            });


        }












    }
}
