using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.Core.Category.Queries;
using MovieLibrary.Data.Repository;

namespace MovieLibrary.Core.Category.Handlers;

public class GetCategoriesHandler : IRequestHandler<GetCategories, IEnumerable<Data.Entities.Category>>
{
    private readonly IRepository<Data.Entities.Category> _categoryRepository;

    public GetCategoriesHandler(IRepository<Data.Entities.Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<Data.Entities.Category>> Handle(GetCategories request, CancellationToken cancellationToken)
    {
        return await _categoryRepository.GetAllAsync();
    }
}