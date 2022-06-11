namespace Totostore.Backend.Application.Catalog.Coupons;

public class GetCouponRequest : IRequest<CouponDto>
{
    public GetCouponRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class CouponByIdSpec : Specification<Coupon, CouponDto>, ISingleResultSpecification
{
    public CouponByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetCouponRequestHandler : IRequestHandler<GetCouponRequest, CouponDto>
{
    private readonly IStringLocalizer<GetCouponRequestHandler> _localizer;
    private readonly IRepository<Coupon> _repository;

    public GetCouponRequestHandler(IRepository<Coupon> repository,
        IStringLocalizer<GetCouponRequestHandler> localizer) => (_repository, _localizer) = (repository, localizer);

    public async Task<CouponDto> Handle(GetCouponRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Coupon, CouponDto>)new CouponByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["coupon.notfound"], request.Id));
}