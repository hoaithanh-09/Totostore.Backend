using Totostore.Backend.Domain.Common.Events;
using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Application.Catalog.Products;

public class UpdateProductRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Rate { get; set; }
    public int Quantity { get; set; }
    public Status Status { get; set; }
    public Guid SupplierId { get; set; }
}

public class UpdateProductRequestHandler : IRequestHandler<UpdateProductRequest, Guid>
{
    private readonly IFileStorageService _file;
    private readonly IStringLocalizer<UpdateProductRequestHandler> _localizer;
    private readonly IRepository<Product> _repository;

    public UpdateProductRequestHandler(IRepository<Product> repository,
        IStringLocalizer<UpdateProductRequestHandler> localizer, IFileStorageService file) =>
        (_repository, _localizer, _file) = (repository, localizer, file);

    public async Task<Guid> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = product ?? throw new NotFoundException(string.Format(_localizer["product.notfound"], request.Id));


        var updatedProduct = product.Update(request.Name, request.Slug, request.Description, request.Rate,
            request.Quantity, request.Status, request.SupplierId);

        // Add Domain Events to be raised after the commit
        product.DomainEvents.Add(EntityUpdatedEvent.WithEntity(product));

        await _repository.UpdateAsync(updatedProduct, cancellationToken);

        return request.Id;
    }
}