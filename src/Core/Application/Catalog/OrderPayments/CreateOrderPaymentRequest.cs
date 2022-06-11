using Totostore.Backend.Application.Catalog.Orders;
using Totostore.Backend.Application.Catalog.Payments;
using Totostore.Backend.Domain.Common.Events;
using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Application.Catalog.OrderPayments;

public class CreateOrderPaymentRequest : IRequest<Guid>
{
    public Guid OrderId { get; set; }
    public Guid PaymentId { get; set; }
    public PaymentStatus Status { get; set; }
    public decimal Amount { get; set; }
    public OrderDto Order { get; set; } = default!;
    public PaymentDto Payment { get; set; } = default!;
}

public class CreateOrderPaymentRequestValidator : CustomValidator<CreateOrderPaymentRequest>
{
    public CreateOrderPaymentRequestValidator(IReadRepository<OrderPayment> orderPaymentepo,
        IReadRepository<Order> orderRepo, IReadRepository<Payment> paymentRepo,
        IStringLocalizer<CreateOrderPaymentRequestValidator> localizer)
    {
        RuleFor(p => p.OrderId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await orderRepo.GetByIdAsync(id, ct) is not null)
            .WithMessage((_, id) => string.Format(localizer["order.notfound"], id));
        RuleFor(p => p.PaymentId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await paymentRepo.GetByIdAsync(id, ct) is not null)
            .WithMessage((_, id) => string.Format(localizer["payment.notfound"], id));
    }
}

public class CreateOrderPaymentRequestHandler : IRequestHandler<CreateOrderPaymentRequest, Guid>
{
    private readonly IFileStorageService _file;
    private readonly IRepository<OrderPayment> _repository;

    public CreateOrderPaymentRequestHandler(IRepository<OrderPayment> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(CreateOrderPaymentRequest request, CancellationToken cancellationToken)
    {
        var orderPayment = new OrderPayment(request.OrderId, request.PaymentId, request.Status, request.Amount);

        // Add Domain Events to be raised after the commit
        orderPayment.DomainEvents.Add(EntityCreatedEvent.WithEntity(orderPayment));

        await _repository.AddAsync(orderPayment, cancellationToken);

        return orderPayment.Id;
    }
}