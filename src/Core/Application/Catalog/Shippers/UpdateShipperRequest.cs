using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Shippers;

public class UpdateShipperRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public DateTime Dob { get; set; } = default!;
    public bool Gender { get; set; }
    public string Mail { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public Guid AddressId { get; set; }
}

public class UpdateShipperRequestValidator : CustomValidator<UpdateShipperRequest>
{
    public UpdateShipperRequestValidator(IReadRepository<Shipper> repository,
        IStringLocalizer<UpdateShipperRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new ShipperByNameSpec(name), ct) is null)
            .WithMessage((_, name) => string.Format(localizer["shipper.alreadyexists"], name));
}

public class UpdateShipperRequestHandler : IRequestHandler<UpdateShipperRequest, Guid>
{
    private readonly IStringLocalizer<UpdateShipperRequestHandler> _localizer;

    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Shipper> _repository;

    public UpdateShipperRequestHandler(IRepositoryWithEvents<Shipper> repository,
        IStringLocalizer<UpdateShipperRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(UpdateShipperRequest request, CancellationToken cancellationToken)
    {
        var shipper = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = shipper ?? throw new NotFoundException(string.Format(_localizer["shipper.notfound"], request.Id));

        shipper.Update(request.Name, request.Dob, request.Gender, request.Mail, request.PhoneNumber, request.AddressId);
        // Add Domain Events to be raised after the commit
        shipper.DomainEvents.Add(EntityUpdatedEvent.WithEntity(shipper));

        await _repository.UpdateAsync(shipper, cancellationToken);

        return request.Id;
    }
}