using Microsoft.AspNetCore.Mvc;

using Application.Dtos.Action;
using Application.Dtos.LogIn;
using Application.Dtos.UserRole;
using Application.IService;
using Application.LDAP;
using Application.Serializer;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace SystemTicketing.Controllers
{
        [Route("api/[controller]/[action]")]
        [ApiController]
        public class AuthenticationController : ControllerBase
        {
            private readonly IUserService _userService;
            private readonly IRoleService _roleService;
            private readonly AuthServices _authenticationService;
            private readonly IUserRoleService _userRoleService;
            private readonly IMapper _mapper;
            private readonly ITokenService _tokenSevice;
            private readonly IJsonFieldsSerializer _jsonFieldsSerializer;

            public AuthenticationController(IUserService userService,
                                 AuthServices authenticationService,
                                 IMapper mapper,
                                 IJsonFieldsSerializer jsonFieldsSerializer,
                                 ITokenService tokenSevice,
                                 IRoleService roleService,
                                 IUserRoleService userRoleService)
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
                // التحقق من المصادقة مع LDAP
                bool isAuthenticated = _authenticationService.Authenticate(loginDto);

                if (!isAuthenticated)
                {
                    return Unauthorized(new ApiResponse(
                        false,
                        "Invalid username or password",
                        StatusCodes.Status401Unauthorized,
                        null));
                }

                // جلب معلومات المستخدم من LDAP
                LdapUserDto ldapUser = _authenticationService.GetUser(loginDto);

                // التحقق من وجود المستخدم في قاعدة بيانات التطبيق
                var existuser = await _userService.GetUserByEmail(ldapUser.Email);

                // إذا لم يكن موجودًا، نقوم بإنشائه
                if (existuser == null)
                {
                    await _userService.InsertUser(ldapUser);

                    // تعيين دور افتراضي (مثل الموظف)
                    CreateUserRole userRoleDto = new CreateUserRole()
                    {
                        UserId = ldapUser.UserID,
                        RoleId = "1" // معرف الدور الافتراضي
                    };

                    await _userRoleService.CreateUserRole(userRoleDto);
                }

                // جلب المستخدم بعد التأكد من وجوده
                var user = await _userService.GetUserByEmail(ldapUser.Email);

                // إنشاء كائن المستخدم للتوكن
                var newUser = new User
                {
                    UserId = user.UserId,
                    Name = ldapUser.Name,
                    Email = ldapUser.Email,
                    Department = ldapUser.Department,
                    // لا نستخدم كلمة المرور من LDAP هنا (نضع قيمة فارغة أو مؤقتة)
                    Password = "LDAP_AUTHENTICATED"
                };

                // إنشاء التوكن
                var token = _tokenSevice.GenerateToken(newUser);

                // جلب الأدوار
                var roles = await _roleService.GetRoleByUserId(user.UserId);
                var roleName = "Employee"; // دور افتراضي

                if (roles != null && roles.Any())
                {
                    roleName = roles.FirstOrDefault()?.Name ?? "Employee";
                }

                var result = new AuthResponseDto
                {
                    Token = token,
                    userId = newUser.UserId,
                    FullName = newUser.Name,
                    Email = newUser.Email,
                    Department = newUser.Department,
                    role = roleName
                };

                return new RawJsonActionResult(_jsonFieldsSerializer.
                    Serialize(new ApiResponse(true, "Signed in successfully", StatusCodes.Status200OK, result), string.Empty));
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

                var userdto = await _userService.GetUserById(userId);
                if (userdto == null)
                {
                    return NotFound(new ApiResponse(
                        false,
                        "User not found",
                        StatusCodes.Status404NotFound,
                        null));
                }

                var roles = await _roleService.GetRoleByUserId(userId);
                var roleName = "Employee"; // دور افتراضي

                if (roles != null && roles.Any())
                {
                    roleName = roles.FirstOrDefault()?.Name ?? "Employee";
                }

                var result = new AuthResponseDto
                {
                    Token = token,
                    userId = userId,
                    FullName = name,
                    Email = email,
                    Department = department,
                    role = roleName ?? "Employee"
                };

                return new RawJsonActionResult(_jsonFieldsSerializer.
                    Serialize(new ApiResponse(true, "Fetched successfully", StatusCodes.Status200OK, result), string.Empty));
            }

            [HttpPost("logout")]
            [Authorize]
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

                return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(
                    true,
                    "Logged out successfully",
                    StatusCodes.Status200OK,
                    null), string.Empty));
            }
       }
    
}
