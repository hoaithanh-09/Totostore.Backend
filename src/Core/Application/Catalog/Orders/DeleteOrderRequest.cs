using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Orders;

public class DeleteOrderRequest : IRequest<Guid>
{
    public DeleteOrderRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class DeleteOrderRequestHandler : IRequestHandler<DeleteOrderRequest, Guid>
{
    private readonly IStringLocalizer<DeleteOrderRequestHandler> _localizer;
    private readonly IRepository<Order> _repository;

    public DeleteOrderRequestHandler(IRepository<Order> repository,
        IStringLocalizer<DeleteOrderRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(DeleteOrderRequest request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = order ?? throw new NotFoundException(_localizer["order.notfound"]);

        // Add Domain Events to be raised after the commit
        order.DomainEvents.Add(EntityDeletedEvent.WithEntity(order));

        await _repository.DeleteAsync(order, cancellationToken);

        return request.Id;
    }
}