using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.OrderProducts;

public class DeleteOrderProductRequest : IRequest<Guid>
{
    public DeleteOrderProductRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class DeleteOrderProductRequestHandler : IRequestHandler<DeleteOrderProductRequest, Guid>
{
    private readonly IStringLocalizer<DeleteOrderProductRequestHandler> _localizer;
    private readonly IRepository<OrderProduct> _repository;

    public DeleteOrderProductRequestHandler(IRepository<OrderProduct> repository,
        IStringLocalizer<DeleteOrderProductRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(DeleteOrderProductRequest request, CancellationToken cancellationToken)
    {
        var orderProduct = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = orderProduct ?? throw new NotFoundException(_localizer["orderProduct.notfound"]);

        // Add Domain Events to be raised after the commit
        orderProduct.DomainEvents.Add(EntityDeletedEvent.WithEntity(orderProduct));

        await _repository.DeleteAsync(orderProduct, cancellationToken);

        return request.Id;
    }
}