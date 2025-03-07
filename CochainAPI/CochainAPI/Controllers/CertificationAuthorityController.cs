using Microsoft.AspNetCore.Mvc;
using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Helpers;

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

        [HttpGet("{documentId}")]
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

        [HttpGet("{documentId}")]
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
    }
}
