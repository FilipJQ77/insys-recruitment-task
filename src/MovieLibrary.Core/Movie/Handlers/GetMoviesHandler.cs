using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.Core.Movie.Queries;
using MovieLibrary.Data.Repository.MovieRepository;

namespace MovieLibrary.Core.Movie.Handlers;

public class GetMoviesHandler : IRequestHandler<GetMovies, IEnumerable<Data.Entities.Movie>>
{
    private MovieRepository _movieRepository;

    public GetMoviesHandler(MovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<IEnumerable<Data.Entities.Movie>> Handle(GetMovies request, CancellationToken cancellationToken)
    {
        return await _movieRepository.GetAllAsync();
    }
}