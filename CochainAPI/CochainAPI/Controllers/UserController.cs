using Microsoft.AspNetCore.Mvc;
using CochainAPI.Model.Authentication;
using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Helpers;
using CochainAPI.Authentication.Interfaces;

namespace RentaloAPI.Controllers
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
            return Ok(await _userService.GetAllActive());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] User userObj)
        {
            userObj.Id = "0";
            return Ok(await _userService.AddAndUpdateUser(userObj));
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(string id, [FromBody] User userObj)
        {
            userObj.Id = id;
            return Ok(await _userService.AddAndUpdateUser(userObj));
        }

        [HttpPost("requestpassword")]
        public async Task<IActionResult> RequestPassword(AuthenticateRequest model)
        {
            var response = await _authService.GenerateTemporaryCredentials(model);

            if (!response)
                return BadRequest(new { message = "Username is incorrect" });

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthenticateRequest model)
        {
            var response = await _authService.SignInWithCredentials(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

    }
}
