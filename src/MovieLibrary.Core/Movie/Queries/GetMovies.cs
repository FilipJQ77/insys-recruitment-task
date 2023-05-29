using System.Collections.Generic;
using MediatR;

namespace MovieLibrary.Core.Movie.Queries;

public record GetMovies():IRequest<IEnumerable<Data.Entities.Movie>>;