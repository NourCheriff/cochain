using Microsoft.AspNetCore.Mvc;
using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Helpers;
using CochainAPI.Model.Documents;

namespace CochainAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
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

        [HttpGet("{id}/{type}")]
        [Authorize]
        public async Task<IActionResult> GetById(string id, string type)
        {
            var response = await _documentService.GetById(id, type);
            if (response == null)
            {
                return BadRequest(new { message = "Document not found" });
            }
            return Ok(response);
        }
    }
}
