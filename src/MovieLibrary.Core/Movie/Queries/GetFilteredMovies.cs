using System.Collections.Generic;
using MediatR;
using MovieLibrary.Data.Entities.Dto;

namespace MovieLibrary.Core.Movie.Queries;

public record GetFilteredMovies(MovieFilterDto MovieFilterDto) : IRequest<IEnumerable<Data.Entities.Movie>>;