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

        [HttpPost("UpdateUser")]
        [Authorize]
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
