using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Abstractions.Services;
using Store.DTO.DTOs.Product;

namespace Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _productService.GetAllAsync();

            return StatusCode((int)response.StatusCode, response.Message);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(ProductPostDTO productPostDTO)
        {
            var response = await _productService.CreateAsync(productPostDTO);

            return StatusCode((int)response.StatusCode, response.Message);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _productService.DeleteAsync(id, true);

            return StatusCode((int)response.StatusCode, response.Message);
        }
    }
}
