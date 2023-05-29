using MediatR;

namespace MovieLibrary.Core.Movie.Commands;

public record PostMovie(Data.Entities.Movie Movie) : IRequest<Data.Entities.Movie>;