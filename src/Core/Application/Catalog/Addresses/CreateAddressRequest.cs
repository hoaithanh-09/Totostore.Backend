using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Addresses;

public class CreateAddressRequest : IRequest<Guid>
{
    public string City { get; set; } = default!;
    public string District { get; set; } = default!;
    public string Ward { get; set; } = default!;
    public string StayingAddress { get; set; } = default!;
}

public class CreateAddressRequestValidator : CustomValidator<CreateAddressRequest>
{
    public CreateAddressRequestValidator(IReadRepository<Address> addressRepo,
        IStringLocalizer<CreateAddressRequestValidator> localizer)
    {
    }
}

public class CreateAddressRequestHandler : IRequestHandler<CreateAddressRequest, Guid>
{
    private readonly IFileStorageService _file;
    private readonly IRepository<Address> _repository;

    public CreateAddressRequestHandler(IRepository<Address> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(CreateAddressRequest request, CancellationToken cancellationToken)
    {
        var address = new Address(request.City, request.District, request.Ward, request.StayingAddress);

        // Add Domain Events to be raised after the commit
        address.DomainEvents.Add(EntityCreatedEvent.WithEntity(address));

        await _repository.AddAsync(address, cancellationToken);

        return address.Id;
    }
}