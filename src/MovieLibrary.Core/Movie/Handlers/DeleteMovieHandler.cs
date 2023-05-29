using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.Core.Movie.Commands;
using MovieLibrary.Data.Repository;

namespace MovieLibrary.Core.Movie.Handlers;

public class DeleteMovieHandler : IRequestHandler<DeleteMovie, bool>
{
    private readonly IRepository<Data.Entities.Movie> _movieRepository;

    public DeleteMovieHandler(IRepository<Data.Entities.Movie> movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<bool> Handle(DeleteMovie request, CancellationToken cancellationToken)
    {
        return await _movieRepository.DeleteAsync(request.Movie);
    }
}