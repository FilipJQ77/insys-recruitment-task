using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.Core.Category.Queries;
using MovieLibrary.Data.Repository;

namespace MovieLibrary.Core.Category.Handlers;

public class GetCategoryHandler : IRequestHandler<GetCategory, Data.Entities.Category>
{
    private readonly IRepository<Data.Entities.Category> _categoryRepository;

    public GetCategoryHandler(IRepository<Data.Entities.Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Data.Entities.Category> Handle(GetCategory request, CancellationToken cancellationToken)
    {
        return await _categoryRepository.GetAsync(request.Id);
    }
}