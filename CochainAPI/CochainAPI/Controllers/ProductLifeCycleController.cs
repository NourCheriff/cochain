using Microsoft.AspNetCore.Mvc;
using CochainAPI.Data.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using CochainAPI.Model.Product;

namespace CochainAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductLifeCycleController : ControllerBase
    {
        private IProductLifeCycleService _productLifeCycleService;

        public ProductLifeCycleController(IProductLifeCycleService productLifeCycleService)
        {
            _productLifeCycleService = productLifeCycleService;
        }

        [HttpGet]
        [Authorize(Policy = "ReadProducts")]
        public async Task<IActionResult> GetCategories()
        {
            var response = await _productLifeCycleService.GetCategories();
            if (response == null)
            {
                return BadRequest(new { message = "Product life cycle categories not found" });
            }
            return Ok(response);
        }

        [HttpGet("LifeCycle/{id}")]
        [Authorize(Policy = "ReadProducts")]
        public async Task<IActionResult> GetProductLifeCycleDocument(Guid id)            
        {
            var response = await _productLifeCycleService.GetProductLifeCyclesByProductInfo(id);
            if (response == null)
            {
                return BadRequest(new { message = "Product life cycle categories not found" });
            }
            return Ok(response);
        }

        [HttpPost("LifeCycle/AddTransport")]
        [Authorize(Policy = "WriteTransportDocument")]
        public async Task<IActionResult> AddProductLifeCycleTransport(ProductLifeCycle productLifeCycle)
        {
            var response = await _productLifeCycleService.AddProductLifeTransport(productLifeCycle);
            if (response == null)
            {
                return BadRequest(new { message = "Product life cycle categories not found" });
            }
            return Ok(response);
        }

        [HttpPost("LifeCycle/AddGeneric")]
        [Authorize(Policy = "WriteProductLifeCycle")] 
        public async Task<IActionResult> AddProductLifeCycleGeneric(ProductLifeCycle productLifeCycle)
        {
            var response = await _productLifeCycleService.AddProductLifeCycle(productLifeCycle);
            if (response == null)
            {
                return BadRequest(new { message = "Product life cycle categories not found" });
            }
            return Ok(response);
        }
    }
}
