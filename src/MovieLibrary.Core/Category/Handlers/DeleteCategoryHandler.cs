using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.Core.Category.Commands;
using MovieLibrary.Data.Repository;

namespace MovieLibrary.Core.Category.Handlers;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategory, bool>
{
    private readonly Repository<Data.Entities.Category> _categoryRepository;

    public DeleteCategoryHandler(Repository<Data.Entities.Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<bool> Handle(DeleteCategory request, CancellationToken cancellationToken)
    {
        return await _categoryRepository.DeleteAsync(request.Category);
    }
}