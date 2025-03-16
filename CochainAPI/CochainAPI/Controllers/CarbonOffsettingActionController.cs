using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Model.CarbonOffset;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CochainAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarbonOffsettingActionController : ControllerBase
    {
        private ICarbonOffsettingActionService _actionService;

        public CarbonOffsettingActionController(ICarbonOffsettingActionService actionService)
        {
            _actionService = actionService;
        }

        [HttpPost]
        [Authorize(Policy = "WriteReadCarbonOffsett")]
        public async Task<IActionResult> AddCarbonOffsettingAction(CarbonOffsettingAction action)
        {
            var response = await _actionService.AddCarbonOffsettingAction(action);
            if (response == null)
            {
                return BadRequest(new { message = "Error on adding action." });
            }
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Policy = "WriteReadCarbonOffsett")]
        public async Task<IActionResult> GetCarbonOffsettingActions(string? scpId, string? queryParam, int? pageNumber, int? pageSize)
        {
            var response = await _actionService.GetCarbonOffsettingActions(scpId, queryParam, pageNumber, pageSize);
            if (response == null)
            {
                return BadRequest(new { message = "Error on adding action." });
            }
            return Ok(response);
        }
    }
}