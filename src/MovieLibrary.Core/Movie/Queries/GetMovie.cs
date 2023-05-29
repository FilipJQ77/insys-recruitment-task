using MediatR;

namespace MovieLibrary.Core.Movie.Queries;

public record GetMovie(int Id) : IRequest<Data.Entities.Movie>;