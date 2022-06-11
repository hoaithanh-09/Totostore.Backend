using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Categories;

public class DeleteCategoryRequest : IRequest<Guid>
{
    public DeleteCategoryRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class DeleteCategoryRequestHandler : IRequestHandler<DeleteCategoryRequest, Guid>
{
    private readonly IStringLocalizer<DeleteCategoryRequestHandler> _localizer;
    private readonly IRepository<Category> _repository;

    public DeleteCategoryRequestHandler(IRepository<Category> repository,
        IStringLocalizer<DeleteCategoryRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = category ?? throw new NotFoundException(_localizer["category.notfound"]);

        // Add Domain Events to be raised after the commit
        category.DomainEvents.Add(EntityDeletedEvent.WithEntity(category));

        await _repository.DeleteAsync(category, cancellationToken);

        return request.Id;
    }
}