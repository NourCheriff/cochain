using Microsoft.AspNetCore.Mvc;
using CochainAPI.Data.Services.Interfaces;
using CochainAPI.Helpers;

namespace CochainAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductLifeCycleController : ControllerBase
    {
        private IProductLifeCycleService _productLifeCycleService;

        public ProductController(IProductLifeCycleService productLifeCycleService)
        {
            _productLifeCycleService = productLifeCycleService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCategories()
        {
            var response = await _productLifeCycleService.GetCategories();
            if (response == null)
            {
                return BadRequest(new { message = "Product life cycle categories not found" });
            }
            return Ok(response);
        }
    }
}
