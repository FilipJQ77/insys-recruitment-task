using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.Core.Category.Commands;
using MovieLibrary.Data.Repository;

namespace MovieLibrary.Core.Category.Handlers;

public class PutCategoryHandler : IRequestHandler<PutCategory, Data.Entities.Category>
{
    private readonly Repository<Data.Entities.Category> _categoryRepository;

    public PutCategoryHandler(Repository<Data.Entities.Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Data.Entities.Category> Handle(PutCategory request, CancellationToken cancellationToken)
    {
        return await _categoryRepository.UpdateAsync(request.Category);
    }
}