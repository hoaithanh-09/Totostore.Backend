using Totostore.Backend.Domain.Common.Events;
using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Application.Catalog.Coupons;

public class UpdateCouponRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public CouponStatus Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime TimeBegin { get; set; }
    public DateTime TimeEnd { get; set; }
    public int Quantity { get; set; }
}

public class UpdateCouponRequestValidator : CustomValidator<UpdateCouponRequest>
{
    public UpdateCouponRequestValidator(IReadRepository<Coupon> repository,
        IStringLocalizer<UpdateCouponRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new CouponByNameSpec(name), ct) is null)
            .WithMessage((_, name) => string.Format(localizer["coupon.alreadyexists"], name));
}

public class UpdateCouponRequestHandler : IRequestHandler<UpdateCouponRequest, Guid>
{
    private readonly IStringLocalizer<UpdateCouponRequestHandler> _localizer;

    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Coupon> _repository;

    public UpdateCouponRequestHandler(IRepositoryWithEvents<Coupon> repository,
        IStringLocalizer<UpdateCouponRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(UpdateCouponRequest request, CancellationToken cancellationToken)
    {
        var coupon = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = coupon ?? throw new NotFoundException(string.Format(_localizer["coupon.notfound"], request.Id));

        coupon.Update(request.Name, request.Type, request.Amount, request.TimeBegin, request.TimeEnd, request.Quantity);
        // Add Domain Events to be raised after the commit
        coupon.DomainEvents.Add(EntityUpdatedEvent.WithEntity(coupon));

        await _repository.UpdateAsync(coupon, cancellationToken);

        return request.Id;
    }
}