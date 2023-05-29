using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.Core.Movie.Commands;
using MovieLibrary.Data.Repository;

namespace MovieLibrary.Core.Movie.Handlers;

public class PostMovieHandler : IRequestHandler<PostMovie, Data.Entities.Movie>
{
    private readonly Repository<Data.Entities.Movie> _movieRepository;

    public PostMovieHandler(Repository<Data.Entities.Movie> movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<Data.Entities.Movie> Handle(PostMovie request, CancellationToken cancellationToken)
    {
        return await _movieRepository.AddAsync(request.Movie);
    }

}