using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Core.Movie.Commands;
using MovieLibrary.Core.Movie.Queries;
using MovieLibrary.Data.Entities;

namespace MovieLibrary.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class MovieManagementController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovieManagementController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            var request = new GetMovies();
            var result = await _mediator.Send(request);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var request = new GetMovie(id);
            var category = await _mediator.Send(request);

            return category != null ? Ok(category) : NotFound();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            var request = new PutMovie(movie);

            try
            {
                var result = await _mediator.Send(request);
                return Ok(result);
            }
            catch (DbUpdateConcurrencyException)
            {
                var getRequest = new GetMovie(id);
                var getCategory = await _mediator.Send(getRequest);
                if (getCategory is null)
                {
                    return NotFound();
                }

                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            var request = new PostMovie(movie);
            var result = await _mediator.Send(request);

            return CreatedAtAction("GetMovie", new {id = movie.Id}, result);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var request = new GetMovie(id);
            var entity = await _mediator.Send(request);
            var deleteRequest = new DeleteMovie(entity);
            var changes = await _mediator.Send(deleteRequest);

            return changes ? NoContent() : NotFound();
        }
    }
}