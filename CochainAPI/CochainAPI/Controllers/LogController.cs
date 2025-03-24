using CochainAPI.Data.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CochainAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="SystemAdmin")]
    public class LogController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLogs(string? severity, string? queryParam, int? pageNumber, int? pageSize)
        {
            var response = await _logService.GetLogs(severity, queryParam, pageNumber, pageSize);
            if (response == null)
            {
                return BadRequest(new { message = "Logs not found" });
            }
            return Ok(response);
        }
    }
}
