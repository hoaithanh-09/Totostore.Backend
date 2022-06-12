using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.ProductDetails;

public class DeleteProductDetailRequest : IRequest<Guid>
{
    public DeleteProductDetailRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class DeleteProductDetailRequestHandler : IRequestHandler<DeleteProductDetailRequest, Guid>
{
    private readonly IStringLocalizer<DeleteProductDetailRequestHandler> _localizer;
    private readonly IRepository<ProductDetail> _repository;

    public DeleteProductDetailRequestHandler(IRepository<ProductDetail> repository,
        IStringLocalizer<DeleteProductDetailRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(DeleteProductDetailRequest request, CancellationToken cancellationToken)
    {
        var productDetail = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = productDetail ?? throw new NotFoundException(_localizer["productDetail.notfound"]);

        // Add Domain Events to be raised after the commit
        productDetail.DomainEvents.Add(EntityDeletedEvent.WithEntity(productDetail));

        await _repository.DeleteAsync(productDetail, cancellationToken);

        return request.Id;
    }
}