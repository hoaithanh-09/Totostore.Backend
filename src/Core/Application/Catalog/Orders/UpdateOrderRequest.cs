using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Orders;

public class UpdateOrderRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string? Note { get; set; }
    public Guid CustomerId { get; set; }
    public Guid? ShipperId { get; set; }
    public Guid AddressDeliveryId { get; set; }
}

public class UpdateOrderRequestValidator : CustomValidator<UpdateOrderRequest>
{
    public UpdateOrderRequestValidator(IReadRepository<Order> orderRepo, IReadRepository<Customer> customerRepo,
        IReadRepository<Address> addressDeliveryRepo, IStringLocalizer<UpdateOrderRequestValidator> localizer)
    {
        RuleFor(p => p.CustomerId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await customerRepo.GetByIdAsync(id, ct) is not null)
            .WithMessage((_, id) => string.Format(localizer["customer.notfound"], id));
        RuleFor(p => p.AddressDeliveryId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await addressDeliveryRepo.GetByIdAsync(id, ct) is not null)
            .WithMessage((_, id) => string.Format(localizer["addressDelivery.notfound"], id));
    }
}

public class UpdateOrderRequestHandler : IRequestHandler<UpdateOrderRequest, Guid>
{
    private readonly IStringLocalizer<UpdateOrderRequestHandler> _localizer;

    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Order> _repository;

    public UpdateOrderRequestHandler(IRepositoryWithEvents<Order> repository,
        IStringLocalizer<UpdateOrderRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(UpdateOrderRequest request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = order ?? throw new NotFoundException(string.Format(_localizer["order.notfound"], request.Id));

        order.Update(request.Amount, request.Note, request.CustomerId, request.ShipperId, request.AddressDeliveryId);
        // Add Domain Events to be raised after the commit
        order.DomainEvents.Add(EntityUpdatedEvent.WithEntity(order));

        await _repository.UpdateAsync(order, cancellationToken);

        return request.Id;
    }
}