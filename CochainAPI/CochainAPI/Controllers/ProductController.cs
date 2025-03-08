using Microsoft.AspNetCore.Mvc;
using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Helpers;
using CochainAPI.Model.Product;

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
        [Authorize(Policy = "ReadProducts")]
        public async Task<IActionResult> GetProducts([FromQuery]string? queryParam, [FromQuery]int? pageNumber, [FromQuery]int? pageSize)
        {
            var response = await _productService.GetProducts(queryParam, pageNumber, pageSize);
            if (response == null)
            {
                return BadRequest(new { message = "Product infos not found" });
            }
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Policy = "AddProducts, ReadProducts")]
        public async Task<IActionResult> GetCategories()
        {
            var response = await _productService.GetCategories();
            if (response == null)
            {
                return BadRequest(new { message = "Product categories not found" });
            }
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Policy = "AddProducts")]
        public async Task<IActionResult> AddProductInfo(ProductInfo productInfo)
        {
            var response = await _productService.AddProductInfo(productInfo);
            if (response == null)
            {
                return BadRequest(new { message = "Product cannot be added" });
            }
            return Ok(response);
        }
    }
}
