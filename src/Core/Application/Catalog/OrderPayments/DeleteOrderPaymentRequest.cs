using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.OrderPayments;

public class DeleteOrderPaymentRequest : IRequest<Guid>
{
    public DeleteOrderPaymentRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class DeleteOrderPaymentRequestHandler : IRequestHandler<DeleteOrderPaymentRequest, Guid>
{
    private readonly IStringLocalizer<DeleteOrderPaymentRequestHandler> _localizer;
    private readonly IRepository<OrderPayment> _repository;

    public DeleteOrderPaymentRequestHandler(IRepository<OrderPayment> repository,
        IStringLocalizer<DeleteOrderPaymentRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(DeleteOrderPaymentRequest request, CancellationToken cancellationToken)
    {
        var orderPayment = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = orderPayment ?? throw new NotFoundException(_localizer["orderPayment.notfound"]);

        // Add Domain Events to be raised after the commit
        orderPayment.DomainEvents.Add(EntityDeletedEvent.WithEntity(orderPayment));

        await _repository.DeleteAsync(orderPayment, cancellationToken);

        return request.Id;
    }
}