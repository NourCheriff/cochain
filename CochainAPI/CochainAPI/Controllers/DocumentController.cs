using Microsoft.AspNetCore.Mvc;
using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Model.Documents;
using Microsoft.AspNetCore.Authorization;

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

        [HttpPost("AddContractDocument")]
        [Authorize(Policy = "WriteContracts")]
        public async Task<IActionResult> AddContractDocument([FromBody] Contract documentObj)
        {
            var response = await _documentService.AddDocument(documentObj);
            if (response == null)
            {
                return BadRequest(new { message = "Document not found" });
            }
            return Ok(response);
        }

        [HttpPost("AddTransportDocument")]
        [Authorize(Policy = "WriteTransportDocument")]
        public async Task<IActionResult> AddTransportDocument([FromBody] ProductLifeCycleDocument documentObj)
        {
            var response = await _documentService.AddDocument(documentObj);
            if (response == null)
            {
                return BadRequest(new { message = "Document not found" });
            }
            return Ok(response);
        }

        [HttpPost("AddCertificationDocument")]
        [Authorize(Policy = "WriteCertificationDocument")]
        public async Task<IActionResult> AddCertificationDocument([FromBody] SupplyChainPartnerCertificate documentObj)
        {
            var response = await _documentService.AddCertificate(documentObj);
            if (response == null)
            {
                return BadRequest(new { message = "Document not found" });
            }
            return Ok(response);
        }

        [HttpPost("AddOriginDocument")]
        [Authorize(Policy = "WriteOriginDocument")]
        public async Task<IActionResult> AddOriginDocument([FromBody] ProductDocument documentObj)
        {
            var response = await _documentService.AddDocument(documentObj);
            if (response == null)
            {
                return BadRequest(new { message = "Document not found" });
            }
            return Ok(response);
        }

        [HttpPost("AddInvoicesDocument")]
        [Authorize(Policy = "WriteInvoices")]
        public async Task<IActionResult> AddInvoicesDocument([FromBody] ProductLifeCycleDocument documentObj)
        {
            var response = await _documentService.AddDocument(documentObj);
            if (response == null)
            {
                return BadRequest(new { message = "Document not found" });
            }
            return Ok(response);
        }

        [HttpGet("{type}/{id}")]
        [Authorize(Policy = "ReadDocuments")]
        public async Task<IActionResult> GetById(string id, string type)
        {
            var response = await _documentService.GetById(id, type);
            if (response == null)
            {
                return BadRequest(new { message = "Document not found" });
            }
            return Ok(response);
        }

        [HttpPost("{type}/{id}")]
        [Authorize(Policy = "RemoveDocuments")]
        public async Task<IActionResult> DeleteDocumentById(Guid id, string fileName, string type)
        {
            var response = await _documentService.DeleteById(id, fileName, type);
            if (!response)
            {
                return BadRequest(new { message = "Document not found" });
            }
            return Ok(response);
        }

        [HttpPost("RemoveCertificate/{id}")]
        [Authorize(Policy = "RemoveCertificationDocument")]
        public async Task<IActionResult> DeleteCertificateById(Guid id, string fileName, string type)
        {
            var response = await _documentService.DeleteCertificateById(id, fileName, type);
            if (!response)
            {
                return BadRequest(new { message = "Document not found" });
            }
            return Ok(response);
        }

        [HttpGet("Sustainability")]
        [Authorize(Policy = "ReadDocuments")]
        public async Task<IActionResult> GetSustainabilityCertificates(string? queryParam, int? pageNumber, int? pageSize)
        {
            var response = await _documentService.GetSustainabilityCertificates(queryParam, pageNumber, pageSize);
            if (response == null)
            {
                return BadRequest(new { message = "Document not found" });
            }
            return Ok(response);
        }

        [HttpGet("EmittedContracts")]
        [Authorize(Policy = "ReadDocuments")]
        public async Task<IActionResult> GetEmittedContracts(string userId, string? queryParam, int? pageNumber, int? pageSize)
        {
            var response = await _documentService.GetEmittedContracts(userId, queryParam, pageNumber, pageSize);
            if (response == null)
            {
                return BadRequest(new { message = "Document not found" });
            }
            return Ok(response);
        }

        [HttpGet("ReceivedContracts")]
        [Authorize(Policy = "ReadDocuments")]
        public async Task<IActionResult> GetReceivedContracts(string scpId, string? queryParam, int? pageNumber, int? pageSize)
        {
            var response = await _documentService.GetReceivedContracts(scpId, queryParam, pageNumber, pageSize);
            if (response == null)
            {
                return BadRequest(new { message = "Document not found" });
            }
            return Ok(response);
        }
    }
}