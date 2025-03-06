using Microsoft.AspNetCore.Mvc;
using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Helpers;

namespace CochainAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet("{id}")]
        [Authorize(Policy = "ReadProducts")]
        public async Task<IActionResult> GetProductsBySCP(Guid id)
        {
            var response = await _productService.GetProductsOfSCP(id);
            if (response == null)
            {
                return BadRequest(new { message = "Product infos not found" });
            }
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Policy ="ReadProducts")]
        public async Task<IActionResult> GetCategories()
        {
            var response = await _productService.GetCategories();
            if (response == null)
            {
                return BadRequest(new { message = "Product categories not found" });
            }
            return Ok(response);
        }
    }
}
