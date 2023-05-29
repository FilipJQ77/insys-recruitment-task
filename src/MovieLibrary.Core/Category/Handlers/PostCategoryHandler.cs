using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MovieLibrary.Core.Category.Commands;
using MovieLibrary.Data.Repository;

namespace MovieLibrary.Core.Category.Handlers;

public class PostCategoryHandler : IRequestHandler<PostCategory, Data.Entities.Category>
{
    private readonly IRepository<Data.Entities.Category> _categoryRepository;

    public PostCategoryHandler(IRepository<Data.Entities.Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Data.Entities.Category> Handle(PostCategory request, CancellationToken cancellationToken)
    {
        return await _categoryRepository.AddAsync(request.Category);
    }
}