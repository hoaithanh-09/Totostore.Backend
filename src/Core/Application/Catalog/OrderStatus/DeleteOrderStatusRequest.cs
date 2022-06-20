using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.OrderStatus;

public class DeleteOrderStatusRequest : IRequest<Guid>
{
    public DeleteOrderStatusRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class DeleteOrderStatusRequestHandler : IRequestHandler<DeleteOrderStatusRequest, Guid>
{
    private readonly IStringLocalizer<DeleteOrderStatusRequestHandler> _localizer;
    private readonly IRepository<Domain.Catalog.OrderStatus> _repository;

    public DeleteOrderStatusRequestHandler(IRepository<Domain.Catalog.OrderStatus> repository,
        IStringLocalizer<DeleteOrderStatusRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(DeleteOrderStatusRequest request, CancellationToken cancellationToken)
    {
        var orderStatus = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = orderStatus ?? throw new NotFoundException(_localizer["orderStatus.notfound"]);

        // Add Domain Events to be raised after the commit
        orderStatus.DomainEvents.Add(EntityDeletedEvent.WithEntity(orderStatus));

        await _repository.DeleteAsync(orderStatus, cancellationToken);

        return request.Id;
    }
}