using System.Collections.Generic;
using System.Threading.Tasks;
using MovieLibrary.Data.Entities;
using MovieLibrary.Data.Entities.Dto;

namespace MovieLibrary.Data.Repository.MovieRepository;

public interface IMovieRepository : IRepository<Movie>
{
    Task<List<Movie>> GetFilteredAsync(MovieFilterDto movieFilterDto);
}