using Totostore.Backend.Application.Catalog.CategoryProducts;
using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Categories;

public class DeleteCategoryRequest : IRequest<Guid>
{
    public DeleteCategoryRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class DeleteCategoryRequestHandler : IRequestHandler<DeleteCategoryRequest, Guid>
{
    private readonly IReadRepository<CategoryProduct> _categoryProductRepo;
    private readonly IStringLocalizer<DeleteCategoryRequestHandler> _localizer;
    private readonly IRepository<Category> _repository;

    public DeleteCategoryRequestHandler(IRepository<Category> repository,
        IStringLocalizer<DeleteCategoryRequestHandler> localizer,
        IReadRepository<CategoryProduct> categoryProductRepo) =>
        (_repository, _localizer, _categoryProductRepo) = (repository, localizer, categoryProductRepo);

    public async Task<Guid> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
    {
        if (await _categoryProductRepo.AnyAsync(new ProductsByCategorySpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_localizer["category.cannotbedeleted"]);
        }

        var category = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = category ?? throw new NotFoundException(_localizer["category.notfound"]);

        // Add Domain Events to be raised after the commit
        category.DomainEvents.Add(EntityDeletedEvent.WithEntity(category));

        await _repository.DeleteAsync(category, cancellationToken);

        return request.Id;
    }
}