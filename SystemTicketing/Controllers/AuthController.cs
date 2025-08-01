using Application.Dtos.Action;
using Application.Dtos.DeviceCategory;
using Application.Dtos.LogIn;
using Application.Dtos.UserRole;
using Application.IService;
using Application.LDAP;
using Application.Serializer;
using AutoMapper;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace SystemTicketing.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly AuthServices _authenticationService;
        private readonly IUserRoleService _userRoleService;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenSevice ;
        private readonly IJsonFieldsSerializer _jsonFieldsSerializer ;
        public AuthController(IUserService userService , AuthServices authenticationService,IMapper mapper,
            IJsonFieldsSerializer jsonFieldsSerializer, ITokenService tokenSevice, IRoleService roleService ,IUserRoleService userRoleService)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
            _userService = userService;
            _tokenSevice = tokenSevice;
            _jsonFieldsSerializer = jsonFieldsSerializer;
            _roleService = roleService;
            _userRoleService = userRoleService;
        }


        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Login(LoginDto loginDto)
        {

            var Isldap = _authenticationService.Authenticate(loginDto);

            if (!Isldap )
            {

                return Unauthorized(" Unauthorized user ");

            }
            
                // get user from Ldap 
                var ldapUser = _authenticationService.GetUser(loginDto);
            
            var existuser = await _userService.GetUserByEmail(ldapUser.
                    Email);

            if (existuser == null)
            {
               
              await _userService.InsertUser(ldapUser);
                CreateUserRole userRoleDto = new CreateUserRole()
                {
                    UserId = ldapUser.UserID,
                    RoleId = "1"
                };

                var userrole = await _userRoleService.CreateUserRole(userRoleDto);


            }
            var user = await _userService.GetUserByEmail(ldapUser.Email);
            var newUser = new User
            {
                UserId = user.UserId,
                Name = ldapUser.Name,
                Email = ldapUser.Email,
                Password = ldapUser.Password,
                Department = ldapUser.Department,
            };
            var token = _tokenSevice.GenerateToken(newUser);


            var roles = await _roleService.GetRoleByUserId(user.UserId);
            var roleName = "Employee"; // Default role

            if (roles != null && roles.Any())
            {
                roleName = roles.FirstOrDefault()?.Name ?? "Employee";
            }

            var result =  new AuthResponseDto
            {
                Token = token,
                userId = newUser.UserId,
                FullName = newUser.Name,
                Email = newUser.Email,
                Department = newUser.Department,
                role= roleName
            };
            return new RawJsonActionResult(_jsonFieldsSerializer.
                Serialize(new ApiResponse(true, "signed in successfuly", StatusCodes.Status200OK, result), string.Empty));


        }





        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> profile()
        {

            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new ApiResponse(
                    false,
                    "Token not found",
                    StatusCodes.Status401Unauthorized,
                    null));
            }

           
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
                var email = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;
                var name = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value;
                var department = jwtToken.Claims.FirstOrDefault(c => c.Type == "department")?.Value;


                var userdto = await  _userService.GetUserById(userId);
            if (userdto == null)
            {
                return NotFound(new ApiResponse(
            false,
            "User not found",
            StatusCodes.Status404NotFound,
            null));
            }
            
            var roles = await _roleService.GetRoleByUserId(userId);
            var roleName = "Employee"; // Default role

            if (roles != null && roles.Any())
            {
                roleName = roles.FirstOrDefault()?.Name ?? "Employee";
            }
            var result = new AuthResponseDto
            {     Token=token,
                userId = userId,
                FullName=name ,
                Email = email,
                Department =department,
                role = roleName ?? "Employee"
            };



            return new RawJsonActionResult(_jsonFieldsSerializer.
                Serialize(new ApiResponse(true, "fetched  successufly", StatusCodes.Status200OK, result), string.Empty));



        
        }




        [HttpPost("logout")]
        [Authorize]
      //  [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Logout()
        {

            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(token))
            {
                return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(
                    false,
                    "Token is missing or invalid",
                    StatusCodes.Status400BadRequest,
                    null), string.Empty));
            }

            // إضافة التوكن إلى القائمة السوداء
            _tokenSevice.AddToBlackListToken(token);

            // لا تستخدم SignOutAsync مع JWT
            return new RawJsonActionResult( _jsonFieldsSerializer.Serialize( new ApiResponse(
                true,
                "Logged out successfully",
                StatusCodes.Status200OK,
                null),string.Empty));

            }
            
           
        }

      

    
}










































//[HttpPost]
//[Authorize]
//[ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
//public IActionResult Logout()
//{
//    var header = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

//    if (!string.IsNullOrEmpty(header)&&header.StartsWith("Berear "))
//    {
//        var token = header.Substring("Bearer ".Length).Trim();
//        _tokenSevice.AddToBlackListToken(token);
//    }





//    return Ok();

//    //return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(
//    //    new ApiResponse(true, "logOut Success", StatusCodes.Status200OK,"Token")));
//}

