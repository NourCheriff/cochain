using Microsoft.AspNetCore.Mvc;
using CochainAPI.Helpers;
using CochainAPI.Data.Services.Interfaces;

namespace CochainAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplyChainPartnerController : ControllerBase
    {
        private ISupplyChainPartnerService _supplychainPartnerService;

        public SupplyChainPartnerController(ISupplyChainPartnerService supplychainPartnerService)
        {
            _supplychainPartnerService = supplychainPartnerService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetTypes()
        {
            var response = await _supplychainPartnerService.GetTypes();
            if (response == null)
            {
                return BadRequest(new { message = "Supply Chain Partner types not found" });
            }
            return Ok(response);
        }
    }
}
