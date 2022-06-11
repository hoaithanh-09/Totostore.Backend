namespace Totostore.Backend.Application.Catalog.OrderCoupons;

public class GetOrderCouponRequest : IRequest<OrderCouponDetailsDto>
{
    public GetOrderCouponRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class OrderCouponByIdSpec : Specification<OrderCoupon, OrderCouponDetailsDto>, ISingleResultSpecification
{
    public OrderCouponByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id)
            .Include(p => p.Order)
            .Include(p => p.Coupon);
}

public class GetOrderCouponRequestHandler : IRequestHandler<GetOrderCouponRequest, OrderCouponDetailsDto>
{
    private readonly IStringLocalizer<GetOrderCouponRequestHandler> _localizer;
    private readonly IRepository<OrderCoupon> _repository;

    public GetOrderCouponRequestHandler(IRepository<OrderCoupon> repository,
        IStringLocalizer<GetOrderCouponRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<OrderCouponDetailsDto>
        Handle(GetOrderCouponRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<OrderCoupon, OrderCouponDetailsDto>)new OrderCouponByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["orderCoupon.notfound"], request.Id));
}