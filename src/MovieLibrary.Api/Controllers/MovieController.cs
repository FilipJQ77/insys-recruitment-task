using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieLibrary.Core.Movie.Queries;
using MovieLibrary.Data.Entities;
using MovieLibrary.Data.Entities.Dto;
using MovieLibrary.Data.Repository.MovieRepository;

namespace MovieLibrary.Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private IMediator _mediator;

        public MovieController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Filter")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetFilteredMovies(MovieFilterDto movieFilterDto)
        {
            var request = new GetFilteredMovies(movieFilterDto);
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}