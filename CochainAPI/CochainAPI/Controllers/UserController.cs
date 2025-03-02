using Microsoft.AspNetCore.Mvc;
using CochainAPI.Model.Authentication;
using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Helpers;
using CochainAPI.Authentication.Interfaces;

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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var response = await _userService.GetAllActive();
            if (response == null)
            {
                return BadRequest(new { message = "Users not found" });
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _userService.GetById(id);
            if (response == null)
            {
                return BadRequest(new { message = "User not found" });
            }
            return Ok(response);
        }

        [HttpPost("UpdateUser")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] User userObj)
        {
            var response = await _userService.AddAndUpdateUser(userObj);
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
    }
}
