using Totostore.Backend.Domain.Common.Events;
using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Application.Catalog.OrderStatuses;

public class CreateOrderStatusRequest : IRequest<Guid>
{
    public OrderStatusEnums Status { get; set; }
    public string? Note { get; set; } = default!;
    public Guid OrderId { get; set; }
}

public class CreateOrderStatusRequestValidator : CustomValidator<CreateOrderStatusRequest>
{
    public CreateOrderStatusRequestValidator(IReadRepository<OrderStatus> orderStatusRepo,
        IReadRepository<Order> orderRepo, IStringLocalizer<CreateOrderStatusRequestValidator> localizer)
    {
        RuleFor(p => p.OrderId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await orderRepo.GetByIdAsync(id, ct) is not null)
            .WithMessage((_, id) => string.Format(localizer["order.notfound"], id));
    }
}

public class CreateOrderStatusRequestHandler : IRequestHandler<CreateOrderStatusRequest, Guid>
{
    private readonly IFileStorageService _file;
    private readonly IRepository<OrderStatus> _repository;

    public CreateOrderStatusRequestHandler(IRepository<OrderStatus> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(CreateOrderStatusRequest request, CancellationToken cancellationToken)
    {
        var orderStatus =
            new OrderStatus(request.Status, request.Note, request.OrderId);

        // Add Domain Events to be raised after the commit
        orderStatus.DomainEvents.Add(EntityCreatedEvent.WithEntity(orderStatus));

        await _repository.AddAsync(orderStatus, cancellationToken);

        return orderStatus.Id;
    }
}