using Microsoft.AspNetCore.Mvc;
using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Helpers;
using CochainAPI.Authentication.Interfaces;
using CochainAPI.Model.Documents;

namespace CochainAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private IDocumentService _documentService;
        private IAuthService _authService;

        public DocumentController(IDocumentService documentService, IAuthService authService)
        {
            _documentService = documentService;
            _authService = authService;
        }

        [HttpPost("UpdateDocument")]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] BaseDocument documentObj)
        {
            var response = await _documentService.AddDocument(documentObj);
            if (response == null)
            {
                return BadRequest(new { message = "Document not found" });
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _documentService.GetById(id);
            if (response == null)
            {
                return BadRequest(new { message = "Document not found" });
            }
            return Ok(response);
        }
    }
}
