using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.Core.Movie.Commands;
using MovieLibrary.Data.Repository.MovieRepository;

namespace MovieLibrary.Core.Movie.Handlers;

public class PutMovieHandler : IRequestHandler<PutMovie, Data.Entities.Movie>
{
    private readonly IMovieRepository _movieRepository;

    public PutMovieHandler(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<Data.Entities.Movie> Handle(PutMovie request, CancellationToken cancellationToken)
    {
        return await _movieRepository.UpdateAsync(request.Movie);
    }
}