using MediatR;

namespace MovieLibrary.Core.Category.Commands;

public record DeleteCategory(Data.Entities.Category Category) : IRequest<bool>;