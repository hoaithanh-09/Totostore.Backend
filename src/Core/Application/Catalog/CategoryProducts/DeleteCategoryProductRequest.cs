using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.CategoryProducts;

public class DeleteCategoryProductRequest : IRequest<Guid>
{
    public DeleteCategoryProductRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class DeleteCategoryProductRequestHandler : IRequestHandler<DeleteCategoryProductRequest, Guid>
{
    private readonly IStringLocalizer<DeleteCategoryProductRequestHandler> _localizer;
    private readonly IRepository<CategoryProduct> _repository;

    public DeleteCategoryProductRequestHandler(IRepository<CategoryProduct> repository,
        IStringLocalizer<DeleteCategoryProductRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(DeleteCategoryProductRequest request, CancellationToken cancellationToken)
    {
        var categoryProduct = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = categoryProduct ?? throw new NotFoundException(_localizer["categoryProduct.notfound"]);

        // Add Domain Events to be raised after the commit
        categoryProduct.DomainEvents.Add(EntityDeletedEvent.WithEntity(categoryProduct));

        await _repository.DeleteAsync(categoryProduct, cancellationToken);

        return request.Id;
    }
}