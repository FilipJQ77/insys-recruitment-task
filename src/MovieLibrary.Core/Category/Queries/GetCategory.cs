using MediatR;

namespace MovieLibrary.Core.Category.Queries;

public record GetCategory(int Id) : IRequest<Data.Entities.Category>;