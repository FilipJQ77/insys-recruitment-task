using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.Core.Movie.Commands;
using MovieLibrary.Data.Repository;
using MovieLibrary.Data.Repository.MovieRepository;

namespace MovieLibrary.Core.Movie.Handlers;

public class PostMovieHandler : IRequestHandler<PostMovie, Data.Entities.Movie>
{
    private readonly IMovieRepository _movieRepository;

    public PostMovieHandler(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<Data.Entities.Movie> Handle(PostMovie request, CancellationToken cancellationToken)
    {
        return await _movieRepository.AddAsync(request.Movie);
    }

}