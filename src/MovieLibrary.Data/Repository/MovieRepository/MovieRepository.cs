using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieLibrary.Data.Entities;
using MovieLibrary.Data.Entities.Dto;

namespace MovieLibrary.Data.Repository.MovieRepository;

public class MovieRepository : Repository<Movie>, IMovieRepository
{
    public MovieRepository(DbContext context) : base(context)
    {
    }

    public Task<List<Movie>> GetFilteredAsync(MovieFilterDto movieFilterDto)
    {
        var queryable = DbSet.AsQueryable();
        if (movieFilterDto.Title is not null)
        {
            queryable = queryable.Where(x => x.Title.Contains(movieFilterDto.Title));
        }

        if (movieFilterDto.Categories is not null && movieFilterDto.Categories.Count > 0)
        {
            queryable = queryable.Where(x =>
                x.MovieCategories.Select(y => y.Category.Name).Intersect(movieFilterDto.Categories).Any());
        }

        if (movieFilterDto.MinImdbRating is not null)
        {
            queryable = queryable.Where(x => x.ImdbRating >= movieFilterDto.MinImdbRating);
        }
        
        if (movieFilterDto.MaxImdbRating is not null)
        {
            queryable = queryable.Where(x => x.ImdbRating <= movieFilterDto.MaxImdbRating);
        }

        return queryable.ToListAsync();
    }
}