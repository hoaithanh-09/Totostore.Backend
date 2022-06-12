using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Shippers;

public class CreateShipperRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public DateTime Dob { get; set; } = default!;
    public bool Gender { get; set; }
    public string Mail { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public Guid AddressId { get; set; }
}

public class CreateShipperRequestValidator : CustomValidator<CreateShipperRequest>
{
    public CreateShipperRequestValidator(IReadRepository<Shipper> repository,
        IStringLocalizer<CreateShipperRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new ShipperByNameSpec(name), ct) is null)
            .WithMessage((_, name) => string.Format(localizer["shipper.alreadyexists"], name));
}

public class CreateShipperRequestHandler : IRequestHandler<CreateShipperRequest, Guid>
{
    private readonly IFileStorageService _file;
    private readonly IRepository<Shipper> _repository;

    public CreateShipperRequestHandler(IRepository<Shipper> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(CreateShipperRequest request, CancellationToken cancellationToken)
    {
        var shipper = new Shipper(request.Name, request.Dob, request.Gender, request.Mail, request.PhoneNumber,
            request.AddressId);

        // Add Domain Events to be raised after the commit
        shipper.DomainEvents.Add(EntityCreatedEvent.WithEntity(shipper));

        await _repository.AddAsync(shipper, cancellationToken);

        return shipper.Id;
    }
}