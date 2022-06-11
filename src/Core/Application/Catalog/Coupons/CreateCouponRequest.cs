using Totostore.Backend.Domain.Common.Events;
using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Application.Catalog.Coupons;

public class CreateCouponRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public CouponStatus Type { get; set; }
    public decimal Amount { get; set; }
    public DateTime TimeBegin { get; set; }
    public DateTime TimeEnd { get; set; }
    public int Quantity { get; set; }
}

public class CreateCouponRequestValidator : CustomValidator<CreateCouponRequest>
{
    public CreateCouponRequestValidator(IReadRepository<Coupon> repository,
        IStringLocalizer<CreateCouponRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new CouponByNameSpec(name), ct) is null)
            .WithMessage((_, name) => string.Format(localizer["coupon.alreadyexists"], name));
}

public class CreateCouponRequestHandler : IRequestHandler<CreateCouponRequest, Guid>
{
    private readonly IFileStorageService _file;
    private readonly IRepository<Coupon> _repository;

    public CreateCouponRequestHandler(IRepository<Coupon> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(CreateCouponRequest request, CancellationToken cancellationToken)
    {
        var coupon = new Coupon(request.Name, request.Type, request.Amount, request.TimeBegin, request.TimeEnd,
            request.Quantity);

        // Add Domain Events to be raised after the commit
        coupon.DomainEvents.Add(EntityCreatedEvent.WithEntity(coupon));

        await _repository.AddAsync(coupon, cancellationToken);

        return coupon.Id;
    }
}