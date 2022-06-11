namespace Totostore.Backend.Application.Catalog.Coupons;

public class SearchCouponsRequest : PaginationFilter, IRequest<PaginationResponse<CouponDto>>
{
    public DateTime? GetNow { get; set; }
}

public class CouponsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Coupon, CouponDto>
{
    public CouponsBySearchRequestSpec(SearchCouponsRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy())
            .Where(x => x.TimeBegin.CompareTo(request.GetNow.Value) <= 0 &&
                        x.TimeEnd.CompareTo(request.GetNow.Value) >= 0);
}

public class SearchCouponsRequestHandler : IRequestHandler<SearchCouponsRequest, PaginationResponse<CouponDto>>
{
    private readonly IReadRepository<Coupon> _repository;

    public SearchCouponsRequestHandler(IReadRepository<Coupon> repository) => _repository = repository;

    public async Task<PaginationResponse<CouponDto>> Handle(SearchCouponsRequest request,
        CancellationToken cancellationToken)
    {
        var spec = new CouponsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}