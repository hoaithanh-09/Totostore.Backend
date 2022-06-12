using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Payments;

public class DeletePaymentRequest : IRequest<Guid>
{
    public DeletePaymentRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class DeletePaymentRequestHandler : IRequestHandler<DeletePaymentRequest, Guid>
{
    private readonly IStringLocalizer<DeletePaymentRequestHandler> _localizer;
    private readonly IRepository<Payment> _repository;

    public DeletePaymentRequestHandler(IRepository<Payment> repository,
        IStringLocalizer<DeletePaymentRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(DeletePaymentRequest request, CancellationToken cancellationToken)
    {
        var payment = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = payment ?? throw new NotFoundException(_localizer["payment.notfound"]);

        // Add Domain Events to be raised after the commit
        payment.DomainEvents.Add(EntityDeletedEvent.WithEntity(payment));

        await _repository.DeleteAsync(payment, cancellationToken);

        return request.Id;
    }
}