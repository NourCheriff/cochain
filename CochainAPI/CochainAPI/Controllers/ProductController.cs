using Microsoft.AspNetCore.Mvc;
using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Model.Product;
using Microsoft.AspNetCore.Authorization;

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

        [HttpGet("scp/{id}")]
        //[Authorize(Policy = "ReadProducts")]
        public async Task<IActionResult> GetProductsBySCP(Guid id)
        {
            var response = await _productService.GetProductsOfSCP(id);
            if (response == null)
            {
                return BadRequest(new { message = "Product infos not found" });
            }
            return Ok(response);
        }

        [HttpGet("allproducts")]
        //[Authorize(Policy = "ReadProducts")]
        public async Task<IActionResult> GetProductsInfo([FromQuery]string? productName, [FromQuery] string? scpName, [FromQuery]int? pageNumber, [FromQuery]int? pageSize)
        {
            var response = await _productService.GetProducts(productName, scpName, pageNumber, pageSize);
            if (response == null)
            {
                return BadRequest(new { message = "Product infos not found" });
            }
            return Ok(response);
        }

        [HttpGet("categories")]
        //[Authorize(Policy = "WriteProducts, ReadProducts")]
        public async Task<IActionResult> GetCategories()
        {
            var response = await _productService.GetCategories();
            if (response == null)
            {
                return BadRequest(new { message = "Product categories not found" });
            }
            return Ok(response);
        }

        [HttpGet("generic")]
        //[Authorize(Policy = "WriteProducts, ReadProducts")]
        public async Task<IActionResult> GetGenericProducts(Guid categoryId)
        {
            var response = await _productService.GetGenericProducts(categoryId);
            if (response == null)
            {
                return BadRequest(new { message = "Generic products not found" });
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        //[Authorize(Policy = "ReadProducts")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var response = await _productService.GetProductById(id);
            if (response == null)
            {
                return BadRequest(new { message = "Product infos not found" });
            }
            return Ok(response);
        }

        [HttpPost]
        //[Authorize(Policy = "WriteProducts")]
        public async Task<IActionResult> AddProductInfo(ProductInfo productInfo)
        {
            //update prodotto può essere fatto solo al proprio prodotto
            var response = await _productService.AddProductInfo(productInfo);
            if (response == null)
            {
                return BadRequest(new { message = "Product cannot be added" });
            }
            return Ok(response);
        }
    }
}
