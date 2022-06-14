namespace Totostore.Backend.Application.Catalog.OrderCoupons;

public class SearchOrderCouponsRequest : PaginationFilter, IRequest<PaginationResponse<OrderCouponDto>>
{
    public Guid? CouponId { get; set; }
}
public class OrderCouponsBySearchRequestSpec : EntitiesByPaginationFilterSpec<OrderCoupon, OrderCouponDto>
{
    public OrderCouponsBySearchRequestSpec(SearchOrderCouponsRequest request)
        : base(request) => Query.Where((x => x.CouponId== request.CouponId.Value));
}

public class SearchOrderCouponsRequestHandler : IRequestHandler<SearchOrderCouponsRequest,
    PaginationResponse<OrderCouponDto>>
{
    private readonly IReadRepository<OrderCoupon> _repository;

    public SearchOrderCouponsRequestHandler(IReadRepository<OrderCoupon> repository) =>
        _repository = repository;

    public async Task<PaginationResponse<OrderCouponDto>> Handle(SearchOrderCouponsRequest request,
        CancellationToken cancellationToken)
    {
        var spec = new OrderCouponsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}