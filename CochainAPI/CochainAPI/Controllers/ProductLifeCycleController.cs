using Microsoft.AspNetCore.Mvc;
using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Helpers;
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

        [HttpGet("LifeCycle/AddTransport")]
        [Authorize(Policy = "ReadProducts")]
        public async Task<IActionResult> AddProductLifeCycleTransport(ProductLifeCycle productLifeCycle)
        {
            var response = await _productLifeCycleService.AddProductLifeCycle(productLifeCycle);
            if (response == null)
            {
                return BadRequest(new { message = "Product life cycle categories not found" });
            }
            return Ok(response);
        }

        [HttpGet("LifeCycle/AddGeneric")]
        [Authorize(Policy = "ReadProducts")]
        public async Task<IActionResult> AddProductLifeCycleInvoice(ProductLifeCycle productLifeCycle)
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
