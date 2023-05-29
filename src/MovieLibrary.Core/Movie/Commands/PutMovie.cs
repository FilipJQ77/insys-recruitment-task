using MediatR;

namespace MovieLibrary.Core.Movie.Commands;

public record PutMovie(Data.Entities.Movie Movie) : IRequest<Data.Entities.Movie>;