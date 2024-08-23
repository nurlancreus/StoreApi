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

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _productService.GetByIdAsync(id);

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductPostDTO productPostDTO)
        {
            var response = await _productService.CreateAsync(productPostDTO);

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] ProductPostDTO updatedCategory)
        {
            var response = await _productService.UpdateAsync(id, updatedCategory);

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, bool soft = true)
        {
            var response = await _productService.DeleteAsync(id, soft);

            return StatusCode((int)response.StatusCode, response);
        }
    }
}
