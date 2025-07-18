using Application.Dtos.Action;
using Application.Dtos.DeviceCategory;
using Application.Dtos.LogIn;
using Application.IService;
using Application.LDAP;
using Application.Serializer;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        private readonly IJsonFieldsSerializer _jsonFieldsSerializer ;
        public AuthController(IUserService userService , AutenticationServices authenticationService,IMapper mapper,
            IJsonFieldsSerializer jsonFieldsSerializer, TokenService tokenSevice)
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
            _userService = userService;
            _tokenSevice = tokenSevice;
            _jsonFieldsSerializer = jsonFieldsSerializer;
        }


        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status200OK)]
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

        var result =    new AuthResponseDto
            {
                Token = token,
                UserId = newUser.UserId,
                FullName = newUser.Name,
                Email = newUser.Email,
                Department = newUser.Department
            };
            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));


        }


        [HttpPost("logout")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Logout()
        {
            
           
                // 1. الحصول على التوكن من رأس الطلب
                var authHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();

                // 2. التحقق من وجود التوكن وصحته
                if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
                {
                    
                    return BadRequest(new ApiResponse(
                        false,
                        "Token is missing or invalid",
                        StatusCodes.Status400BadRequest,
                        null));
                }

                // 3. استخراج التوكن من الرأس
                var token = authHeader.Substring("Bearer ".Length).Trim();

                // 4. إضافة التوكن إلى القائمة السوداء
                 _tokenSevice.AddToBlackListToken(token);

                // 5. تسجيل الخروج من نظام المصادقة
                await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);

            // 6. إزالة ملفات تعريف الارتباط إذا كنت تستخدمها
            //  Response.Cookies.Delete("access_token");

            //  _logger.LogInformation($"User with token: {token.Substring(0, 5)}... logged out successfully");

            // 7. إرجاع النتيجة الناجحة
            return Ok(new ApiResponse(true, "successful", StatusCodes.Status200OK, null));
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










    
}
