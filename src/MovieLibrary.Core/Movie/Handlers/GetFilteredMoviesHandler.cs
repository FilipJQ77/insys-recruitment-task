using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.Core.Movie.Queries;
using MovieLibrary.Data.Repository.MovieRepository;

namespace MovieLibrary.Core.Movie.Handlers;

public class GetFilteredMoviesHandler : IRequestHandler<GetFilteredMovies, IEnumerable<Data.Entities.Movie>>
{
    private readonly MovieRepository _movieRepository;

    public GetFilteredMoviesHandler(MovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<IEnumerable<Data.Entities.Movie>> Handle(GetFilteredMovies request, CancellationToken cancellationToken)
    {
        return await _movieRepository.GetFilteredAsync(request.MovieFilterDto);
    }
}