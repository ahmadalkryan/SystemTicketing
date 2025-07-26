using Application.Dtos.Action;
using Application.Dtos.LogIn;
using Application.Dtos.Role;
using Application.IService;
using Application.LDAP;
using Application.Serializer;
using Infrastructure.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SystemTicketing.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJsonFieldsSerializer _jsonFieldsSerializer;

        public UserController(IUserService userService ,IJsonFieldsSerializer jsonFieldsSerializer)
        {
            _userService = userService; 
            _jsonFieldsSerializer = jsonFieldsSerializer;
            
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<UserDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsers();

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }



        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Insertuser( LdapUser ldapUser )
        {
            var result = await _userService.InsertUser(ldapUser);

            return new RawJsonActionResult(_jsonFieldsSerializer.Serialize(new ApiResponse(true, "", StatusCodes.Status200OK, result), string.Empty));
        }



    }
}
