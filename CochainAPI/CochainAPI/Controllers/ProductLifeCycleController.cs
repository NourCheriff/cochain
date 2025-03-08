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

        public ProductLifeCycleController(IProductLifeCycleService productLifeCycleService)
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

        [HttpGet]
        [Authorize(Policy = "ReadProducts")]
        public async Task<IActionResult> GetProductLifeCycleDocument()            
        {
            //se type fattura o ddt solo i diretti interessati e admin di sistema
            var response = await _productLifeCycleService.GetCategories();
            if (response == null)
            {
                return BadRequest(new { message = "Product life cycle categories not found" });
            }
            return Ok(response);
        }
    }
}
