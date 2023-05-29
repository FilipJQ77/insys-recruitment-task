using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Core.Category.Commands;
using MovieLibrary.Core.Category.Queries;
using MovieLibrary.Data.Entities;

namespace MovieLibrary.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class CategoryManagementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryManagementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var request = new GetCategories();
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var request = new GetCategory(id);
            var category = await _mediator.Send(request);

            return category != null ? Ok(category) : NotFound();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            var request = new PutCategory(category);

            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (DbUpdateConcurrencyException)
            {
                var getRequest = new GetCategory(id);
                var getCategory = await _mediator.Send(getRequest);
                if (getCategory is null)
                {
                    return NotFound();
                }

                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            var request = new PostCategory(category);
            var result = await _mediator.Send(request);

            return CreatedAtAction("GetCategory", new {id = category.Id}, result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var request = new GetCategory(id);
            var entity = await _mediator.Send(request);
            var deleteRequest = new DeleteCategory(entity);
            var changes = await _mediator.Send(deleteRequest);

            return changes ? NoContent() : NotFound();
        }
    }
}