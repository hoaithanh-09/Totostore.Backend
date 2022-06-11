using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.OrderCoupons;

public class CreateOrderCouponRequest : IRequest<Guid>
{
    public Guid OrderId { get; set; }
    public Guid CouponId { get; set; }
}

public class CreateOrderCouponRequestValidator : CustomValidator<CreateOrderCouponRequest>
{
    public CreateOrderCouponRequestValidator(IReadRepository<OrderCoupon> orderCouponRepo,
        IReadRepository<Order> orderRepo, IReadRepository<Coupon> couponRepo,
        IStringLocalizer<CreateOrderCouponRequestValidator> localizer)
    {
        RuleFor(p => p.OrderId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await orderRepo.GetByIdAsync(id, ct) is not null)
            .WithMessage((_, id) => string.Format(localizer["order.notfound"], id));
        RuleFor(p => p.CouponId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await couponRepo.GetByIdAsync(id, ct) is not null)
            .WithMessage((_, id) => string.Format(localizer["coupon.notfound"], id));
    }
}

public class CreateOrderCouponRequestHandler : IRequestHandler<CreateOrderCouponRequest, Guid>
{
    private readonly IFileStorageService _file;
    private readonly IRepository<OrderCoupon> _repository;

    public CreateOrderCouponRequestHandler(IRepository<OrderCoupon> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(CreateOrderCouponRequest request, CancellationToken cancellationToken)
    {
        var orderCoupon = new OrderCoupon(request.OrderId, request.CouponId);

        // Add Domain Events to be raised after the commit
        orderCoupon.DomainEvents.Add(EntityCreatedEvent.WithEntity(orderCoupon));

        await _repository.AddAsync(orderCoupon, cancellationToken);

        return orderCoupon.Id;
    }
}