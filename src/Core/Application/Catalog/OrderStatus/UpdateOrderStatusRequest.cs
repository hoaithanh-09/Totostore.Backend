using Totostore.Backend.Domain.Common.Events;
using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Application.Catalog.OrderStatus;

public class UpdateOrderStatusRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public OrderStatusEnums Status { get; set; }
    public string? Note { get; set; } = default!;
    public Guid OrderId { get; set; }
}

public class UpdateOrderStatusRequestValidator : CustomValidator<UpdateOrderStatusRequest>
{
    public UpdateOrderStatusRequestValidator(IReadRepository<Domain.Catalog.OrderStatus> orderStatusRepo,
        IReadRepository<Order> orderRepo, IStringLocalizer<UpdateOrderStatusRequestValidator> localizer)
    {
        RuleFor(p => p.OrderId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await orderRepo.GetByIdAsync(id, ct) is not null)
            .WithMessage((_, id) => string.Format(localizer["order.notfound"], id));
    }
}

public class UpdateOrderStatusRequestHandler : IRequestHandler<UpdateOrderStatusRequest, Guid>
{
    private readonly IStringLocalizer<UpdateOrderStatusRequestHandler> _localizer;

    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Domain.Catalog.OrderStatus> _repository;

    public UpdateOrderStatusRequestHandler(IRepositoryWithEvents<Domain.Catalog.OrderStatus> repository,
        IStringLocalizer<UpdateOrderStatusRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(UpdateOrderStatusRequest request, CancellationToken cancellationToken)
    {
        var orderStatus = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = orderStatus ?? throw new NotFoundException(string.Format(_localizer["orderStatus.notfound"], request.Id));

        orderStatus.Update(request.Status, request.Note, request.OrderId);
        // Add Domain Events to be raised after the commit
        orderStatus.DomainEvents.Add(EntityUpdatedEvent.WithEntity(orderStatus));

        await _repository.UpdateAsync(orderStatus, cancellationToken);

        return request.Id;
    }
}

