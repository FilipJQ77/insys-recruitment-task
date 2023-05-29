using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Data.Entities;
using MovieLibrary.Data.Entities.Dto;
using MovieLibrary.Data.Repository.MovieRepository;

namespace MovieLibrary.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private IMovieRepository MovieRepository { get; }

        public MovieController(IMovieRepository movieRepository)
        {
            MovieRepository = movieRepository;
        }

        [HttpGet("Filter")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetFilteredMovies(MovieFilterDto movieFilterDto)
        {
            return await MovieRepository.GetFilteredAsync(movieFilterDto);
        }
    }
}