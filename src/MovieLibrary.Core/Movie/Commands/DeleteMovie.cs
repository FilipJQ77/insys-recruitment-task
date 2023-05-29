using MediatR;

namespace MovieLibrary.Core.Movie.Commands;

public record DeleteMovie(Data.Entities.Movie Movie) : IRequest<bool>;