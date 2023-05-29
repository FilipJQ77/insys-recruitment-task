using System.Collections.Generic;
using MediatR;

namespace MovieLibrary.Core.Category.Queries;

public record GetCategories : IRequest<IEnumerable<Data.Entities.Category>>;