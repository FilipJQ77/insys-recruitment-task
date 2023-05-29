using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.Core.Movie.Queries;
using MovieLibrary.Data.Repository.MovieRepository;

namespace MovieLibrary.Core.Movie.Handlers;

public class GetMovieHandler : IRequestHandler<GetMovie, Data.Entities.Movie>
{
    private readonly IMovieRepository _movieRepository;

    public GetMovieHandler(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<Data.Entities.Movie> Handle(GetMovie request, CancellationToken cancellationToken)
    {
        return await _movieRepository.GetAsync(request.Id);
    }
}