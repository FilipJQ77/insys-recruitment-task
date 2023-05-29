using MediatR;

namespace MovieLibrary.Core.Category.Commands;

public record PostCategory(Data.Entities.Category Category) : IRequest<Data.Entities.Category>;