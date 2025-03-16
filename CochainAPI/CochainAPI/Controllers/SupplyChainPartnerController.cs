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

        [HttpGet("categories")]
        //[Authorize(Policy = "ReadSCP")]
        public async Task<IActionResult> GetTypes()
        {
            var response = await _supplychainPartnerService.GetTypes();
            if (response == null)
            {
                return BadRequest(new { message = "Supply Chain Partner types not found" });
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        //[Authorize(Policy = "ReadSCP")]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _supplychainPartnerService.GetTypes();
            if (response == null)
            {
                return BadRequest(new { message = "Supply Chain Partner types not found" });
            }
            return Ok(response);
        }

        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> GetSupplyChainPartner(string? queryparam, int? pageNumber, int? pageSize)
        {
            var response = await _supplychainPartnerService.GetSupplyChainPartners(queryparam, pageNumber, pageSize);
            if (response == null)
            {
                return BadRequest(new { message = "Supply chain partner not found" });
            }
            return Ok(response);
        }

        [HttpPost("addSCP")]
        //[Authorize(Policy = "WriteSCP")]
        public async Task<IActionResult> AddSupplyChainPartner()
        {
            var response = await _supplychainPartnerService.GetTypes();
            if (response == null)
            {
                return BadRequest(new { message = "Supply Chain Partner types not found" });
            }
            return Ok(response);
        }

        [HttpPost("updateSCP")]
        //[Authorize(Policy ="UpdateSCP")]
        public async Task<IActionResult> UpdateSupplyChainPartner()
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
