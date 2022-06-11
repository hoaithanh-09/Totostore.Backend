using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Coupons;

public class DeleteCouponRequest : IRequest<Guid>
{
    public DeleteCouponRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class DeleteCouponRequestHandler : IRequestHandler<DeleteCouponRequest, Guid>
{
    private readonly IStringLocalizer<DeleteCouponRequestHandler> _localizer;
    private readonly IRepository<Coupon> _repository;

    public DeleteCouponRequestHandler(IRepository<Coupon> repository,
        IStringLocalizer<DeleteCouponRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(DeleteCouponRequest request, CancellationToken cancellationToken)
    {
        var coupon = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = coupon ?? throw new NotFoundException(_localizer["coupon.notfound"]);

        // Add Domain Events to be raised after the commit
        coupon.DomainEvents.Add(EntityDeletedEvent.WithEntity(coupon));

        await _repository.DeleteAsync(coupon, cancellationToken);

        return request.Id;
    }
}