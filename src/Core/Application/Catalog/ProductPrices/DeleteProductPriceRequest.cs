using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.ProductPrices;

public class DeleteProductPriceRequest : IRequest<Guid>
{
    public DeleteProductPriceRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class DeleteProductPriceRequestHandler : IRequestHandler<DeleteProductPriceRequest, Guid>
{
    private readonly IStringLocalizer<DeleteProductPriceRequestHandler> _localizer;
    private readonly IRepository<ProductPrice> _repository;

    public DeleteProductPriceRequestHandler(IRepository<ProductPrice> repository,
        IStringLocalizer<DeleteProductPriceRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(DeleteProductPriceRequest request, CancellationToken cancellationToken)
    {
        var productPrice = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = productPrice ?? throw new NotFoundException(_localizer["productPrice.notfound"]);

        // Add Domain Events to be raised after the commit
        productPrice.DomainEvents.Add(EntityDeletedEvent.WithEntity(productPrice));

        await _repository.DeleteAsync(productPrice, cancellationToken);

        return request.Id;
    }
}