using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.Core.Abstractions.Repositories;
using Store.Core.Entities;

namespace Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryReadRepository _readRepository;
        private readonly ICategoryWriteRepository _writeRepository;

        public CategoriesController(ICategoryReadRepository readRepository, ICategoryWriteRepository writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Category> categories = await _readRepository.GetAll(false).ToListAsync();

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Category? category = await _readRepository.GetByIdAsync(id, false);

            if (category == null)
            {
                return NotFound(category);
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category postedCategory)
        {
            bool isAdded = await _writeRepository.AddAsync(postedCategory);

            if (isAdded)
            {
                await _writeRepository.SaveChangesAsync();
                return Created();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Category updatedCategory)
        {
            Category? category = await _readRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound(category);
            }

            category.Name = updatedCategory.Name;
            bool isUpdated = _writeRepository.Update(category);

            if (isUpdated)
            {
                await _writeRepository.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id, [FromQuery] bool soft = false)
        {
            Category? category = await _readRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound(category);
            }

            bool isDeleted = soft switch
            {
                false => _writeRepository.Delete(category),
                true => _writeRepository.DeleteSoft(category),
            };

            if (isDeleted)
            {
                await _writeRepository.SaveChangesAsync();
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
