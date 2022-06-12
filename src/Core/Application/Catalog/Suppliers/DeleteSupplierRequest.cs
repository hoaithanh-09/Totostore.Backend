using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Suppliers;

public class DeleteSupplierRequest : IRequest<Guid>
{
    public DeleteSupplierRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class DeleteSupplierRequestHandler : IRequestHandler<DeleteSupplierRequest, Guid>
{
    private readonly IStringLocalizer<DeleteSupplierRequestHandler> _localizer;
    private readonly IRepository<Supplier> _repository;

    public DeleteSupplierRequestHandler(IRepository<Supplier> repository,
        IStringLocalizer<DeleteSupplierRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(DeleteSupplierRequest request, CancellationToken cancellationToken)
    {
        var supplier = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = supplier ?? throw new NotFoundException(_localizer["supplier.notfound"]);

        // Add Domain Events to be raised after the commit
        supplier.DomainEvents.Add(EntityDeletedEvent.WithEntity(supplier));

        await _repository.DeleteAsync(supplier, cancellationToken);

        return request.Id;
    }
}