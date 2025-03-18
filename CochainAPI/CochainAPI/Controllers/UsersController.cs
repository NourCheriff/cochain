using Microsoft.AspNetCore.Mvc;
using CochainAPI.Model.Authentication;
using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Authentication.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CochainAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IAuthService _authService;

        public UsersController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpPost("AddUser")]
        [Authorize(Policy = "AddUser")]
        public async Task<IActionResult> AddUser([FromBody] User userObj)
        {
            var response = await _userService.AddUser(userObj);
            if (response == null)
            {
                return BadRequest(new { message = "User not found" });
            }
            return Ok(response);
        }

        [HttpPost("UpdateUser")]
        [Authorize(Policy = "AddUser")]
        public async Task<IActionResult> UpdateUser([FromBody] User userObj)
        {
            var response = await _userService.UpdateUser(userObj);
            if (response == null)
            {
                return BadRequest(new { message = "User not found" });
            }
            return Ok(response);
        }

        [HttpPost("RequestPassword")]
        public async Task<IActionResult> RequestPassword(AuthenticateRequest model)
        {
            var response = await _authService.GenerateTemporaryCredentials(model);
            if (!response)
                return BadRequest(new { message = "Username is incorrect" });
            
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(AuthenticateRequest model)
        {
            var response = await _authService.SignInWithCredentials(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("company/{companyId}")]
        [Authorize(Roles = "SystemAdmin")]
        public async Task<IActionResult> GetUsersByCompanyId(Guid companyId, [FromQuery] string? companyType)
        {
            var response = await _userService.GetUsersByCompanyId(companyId, companyType);
            if (response == null)
            {
                return BadRequest(new { message = "Users not found" });
            }
            return Ok(response);
        }

        [HttpPost("DeleteUser/{id}")]
        [Authorize(Policy = "RemoveUser")]
        public async Task<IActionResult> DeleteUserById(Guid id)
        {
            var response = await _userService.DeleteById(id);
            if (!response)
            {
                return BadRequest(new { message = "User not found" });
            }
            return Ok(response);
        }
    }
}