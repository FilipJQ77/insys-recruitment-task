using MediatR;

namespace MovieLibrary.Core.Category.Commands;

public record PutCategory(Data.Entities.Category Category) : IRequest<Data.Entities.Category>;