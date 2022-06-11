using Totostore.Backend.Application.Catalog.Addresses;
using Totostore.Backend.Application.Catalog.Customers;
using Totostore.Backend.Application.Catalog.Shippers;
using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Orders;

public class CreateOrderRequest : IRequest<Guid>
{
    public decimal Amount { get; set; }
    public string? Note { get; set; }
    public Guid CustomerId { get; set; }
    public Guid? ShipperId { get; set; }
    public Guid AddressDeliveryId { get; set; }
    public CustomerDto Customer { get; set; } = default!;
    public ShipperDto Shipper { get; set; } = default!;
    public AddressDto Address { get; set; } = default!;
}

public class CreateOrderRequestValidator : CustomValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator(IReadRepository<Order> orderRepo, IReadRepository<Customer> customerRepo,
        IReadRepository<Address> addressDeliveryRepo, IStringLocalizer<CreateOrderRequestValidator> localizer)
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

public class CreateOrderRequestHandler : IRequestHandler<CreateOrderRequest, Guid>
{
    private readonly IFileStorageService _file;
    private readonly IRepository<Order> _repository;

    public CreateOrderRequestHandler(IRepository<Order> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var order = new Order(request.Amount, request.Note, request.CustomerId, request.ShipperId,
            request.AddressDeliveryId);

        // Add Domain Events to be raised after the commit
        order.DomainEvents.Add(EntityCreatedEvent.WithEntity(order));

        await _repository.AddAsync(order, cancellationToken);

        return order.Id;
    }
}