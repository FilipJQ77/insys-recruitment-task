using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data.Entities;
using MovieLibrary.Data.Repository;

namespace MovieLibrary.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class MovieManagementController : ControllerBase
    {
        private IRepository<Movie> MovieRepository { get; }

        public MovieManagementController(IRepository<Movie> movieRepository)
        {
            MovieRepository = movieRepository;
        }

        // GET: v1/MovieManagement
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await MovieRepository.GetAllAsync();
        }

        // GET: v1/MovieManagement/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await MovieRepository.GetAsync(id);

            return movie != null ? movie : NotFound();
        }

        // PUT: v1/MovieManagement/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            try
            {
                await MovieRepository.UpdateAsync(movie);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (MovieRepository.GetAsync(id) is null)
                {
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: v1/MovieManagement
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            await MovieRepository.AddAsync(movie);

            return CreatedAtAction("GetMovie", new {id = movie.Id}, movie);
        }

        // DELETE: v1/MovieManagement/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await MovieRepository.GetAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            await MovieRepository.DeleteAsync(movie);

            return NoContent();
        }
    }
}