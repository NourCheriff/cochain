using Microsoft.AspNetCore.Mvc;
using CochainAPI.Data.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using CochainAPI.Model.CompanyEntities;

namespace CochainAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificationAuthorityController : ControllerBase
    {
        private ICertificationAuthorityService _certificationAuthorityService;

        public CertificationAuthorityController(ICertificationAuthorityService certificationAuthorityService)
        {
            _certificationAuthorityService = certificationAuthorityService;
        }

        [HttpGet]
        [Authorize(Policy = "ReadCA")]
        public async Task<IActionResult> GetCertificationAuthority(string? queryparam, int? pageNumber, int? pageSize)
        {
            var response = await _certificationAuthorityService.GetCertificationAuthorities(queryparam, pageNumber, pageSize);
            if (response == null)
            {
                return BadRequest(new { message = "Certificates not found" });
            }
            return Ok(response);
        }

        [HttpPost("AddCA")]
        [Authorize(Policy = "WriteCA")]
        public async Task<IActionResult> AddCertificationAuthority([FromBody] CertificationAuthority certificationAuthority)
        {
            var response = await _certificationAuthorityService.AddCertificationAuthority(certificationAuthority);
            if (response == null)
            {
                return BadRequest(new { message = "Error on certification authority insertion" });
            }
            return Ok(response);

        }
    }
}
