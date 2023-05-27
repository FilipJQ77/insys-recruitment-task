using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data.Entities;
using MovieLibrary.Data.Repository;

namespace MovieLibrary.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class CategoryManagementController : ControllerBase
    {
        private IRepository<Category> CategoryRepository { get; }

        public CategoryManagementController(IRepository<Category> categoryRepository)
        {
            CategoryRepository = categoryRepository;
        }

        // GET: v1/CategoryManagement
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await CategoryRepository.GetAllAsync();
        }

        // GET: v1/CategoryManagement/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await CategoryRepository.GetAsync(id);

            return category != null ? category : NotFound();
        }

        // PUT: v1/CategoryManagement/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            try
            {
                await CategoryRepository.UpdateAsync(category);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (CategoryRepository.GetAsync(id) is null)
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: v1/CategoryManagement
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            await CategoryRepository.AddAsync(category);

            return CreatedAtAction("GetCategory", new {id = category.Id}, category);
        }

        // DELETE: v1/CategoryManagement/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await CategoryRepository.GetAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await CategoryRepository.DeleteAsync(category);

            return NoContent();
        }
    }
}
