using Totostore.Backend.Application.Catalog.Products;
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
    private readonly IRepository<Supplier> _supplierRepo;
    private readonly IReadRepository<Product> _productRepo;

    public DeleteSupplierRequestHandler(IRepository<Supplier> supplierRepo, IReadRepository<Product> productRepo, IStringLocalizer<DeleteSupplierRequestHandler> localizer) =>
        (_supplierRepo, _productRepo, _localizer) = (supplierRepo, productRepo, localizer);

    public async Task<Guid> Handle(DeleteSupplierRequest request, CancellationToken cancellationToken)
    {
        if (await _productRepo.AnyAsync(new ProductsBySupplierSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_localizer["supplier.cannotbedeleted"]);
        }

        var supplier = await _supplierRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = supplier ?? throw new NotFoundException(_localizer["supplier.notfound"]);

        // Add Domain Events to be raised after the commit
        supplier.DomainEvents.Add(EntityDeletedEvent.WithEntity(supplier));

        await _supplierRepo.DeleteAsync(supplier, cancellationToken);

        return request.Id;
    }
}