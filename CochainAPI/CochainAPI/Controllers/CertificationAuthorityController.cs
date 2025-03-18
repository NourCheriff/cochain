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

        [HttpGet("{certificationAuthorityId}")]
        [Authorize]
        public async Task<IActionResult> GetSustainabilityCertificate(string certificationAuthorityId)
        {
            var response = await _certificationAuthorityService.GetSustainabilityCertificate(certificationAuthorityId);
            if (response == null)
            {
                return BadRequest(new { message = "Certificates not found" });
            }
            return Ok(response);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCertificationAuthority(string? queryparam, int? pageNumber, int? pageSize)
        {
            var response = await _certificationAuthorityService.GetCertificationAuthorities(queryparam, pageNumber, pageSize);
            if (response == null)
            {
                return BadRequest(new { message = "Certificates not found" });
            }
            return Ok(response);
        }

        [HttpGet("documents/{documentId}")]
        [Authorize]
        public async Task<IActionResult> UpdateSustainabilityCertificate(string documentId)
        {
            var response = await _certificationAuthorityService.UpdateSustainabilityCertificate(documentId);
            if (response == null)
            {
                return BadRequest(new { message = "Cannot update certificate" });
            }
            return Ok(response);
        }

        [HttpPost("documents/{documentId}")]
        [Authorize]
        public async Task<IActionResult> DeleteSustainabilityCertificate(string documentId)
        {
            var response = await _certificationAuthorityService.UpdateSustainabilityCertificate(documentId);
            if (response == null)
            {
                return BadRequest(new { message = "Cannot delete certificate not found" });
            }
            return Ok(response);
        }

        [HttpPost("AddCA")]
        //[Authorize(Policy = "WriteCA")]
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
