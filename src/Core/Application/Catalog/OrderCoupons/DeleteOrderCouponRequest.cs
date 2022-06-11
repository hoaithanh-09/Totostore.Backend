using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.OrderCoupons;

public class DeleteOrderCouponRequest : IRequest<Guid>
{
    public DeleteOrderCouponRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class DeleteOrderCouponRequestHandler : IRequestHandler<DeleteOrderCouponRequest, Guid>
{
    private readonly IStringLocalizer<DeleteOrderCouponRequestHandler> _localizer;
    private readonly IRepository<OrderCoupon> _repository;

    public DeleteOrderCouponRequestHandler(IRepository<OrderCoupon> repository,
        IStringLocalizer<DeleteOrderCouponRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(DeleteOrderCouponRequest request, CancellationToken cancellationToken)
    {
        var orderCoupon = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = orderCoupon ?? throw new NotFoundException(_localizer["orderCoupon.notfound"]);

        // Add Domain Events to be raised after the commit
        orderCoupon.DomainEvents.Add(EntityDeletedEvent.WithEntity(orderCoupon));

        await _repository.DeleteAsync(orderCoupon, cancellationToken);

        return request.Id;
    }
}