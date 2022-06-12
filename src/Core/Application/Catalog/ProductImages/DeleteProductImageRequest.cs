using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.ProductImages;

public class DeleteProductImageRequest : IRequest<Guid>
{
    public DeleteProductImageRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class DeleteProductImageRequestHandler : IRequestHandler<DeleteProductImageRequest, Guid>
{
    private readonly IStringLocalizer<DeleteProductImageRequestHandler> _localizer;
    private readonly IRepository<ProductImage> _repository;

    public DeleteProductImageRequestHandler(IRepository<ProductImage> repository,
        IStringLocalizer<DeleteProductImageRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(DeleteProductImageRequest request, CancellationToken cancellationToken)
    {
        var productImage = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = productImage ?? throw new NotFoundException(_localizer["productImage.notfound"]);

        // Add Domain Events to be raised after the commit
        productImage.DomainEvents.Add(EntityDeletedEvent.WithEntity(productImage));

        await _repository.DeleteAsync(productImage, cancellationToken);

        return request.Id;
    }
}