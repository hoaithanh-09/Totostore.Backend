using Totostore.Backend.Application.Identity.Users;
using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Customers;

public class CreateCustomerRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public DateTime Dob { get; set; } = default!;
    public bool Gender { get; set; }
    public string Mail { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
   // public Guid AddressId { get; set; }
    public string UserId { get; set; }
    public string City { get; set; } = default!;
    public string District { get; set; } = default!;
    public string Ward { get; set; } = default!;
    public string StayingAddress { get; set; } = default!;
}

public class CreateCustomerRequestValidator : CustomValidator<CreateCustomerRequest>
{
    public CreateCustomerRequestValidator(IReadRepository<Customer> repository,
        IStringLocalizer<CreateCustomerRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new CustomerByNameSpec(name), ct) is null)
            .WithMessage((_, name) => string.Format(localizer["customer.alreadyexists"], name));
}

public class CreateCustomerRequestHandler : IRequestHandler<CreateCustomerRequest, Guid>
{
    private readonly IFileStorageService _file;
    private readonly IRepository<Customer> _repository;
    private readonly IRepository<Address> _addressRepository;

    public CreateCustomerRequestHandler(IRepository<Customer> repository, IFileStorageService file, IRepository<Address> addressRepository) =>
        (_repository, _file, _addressRepository) = (repository, file, addressRepository);

    public async Task<Guid> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var address = new Address(request.City, request.District, request.Ward, request.StayingAddress);
        address.DomainEvents.Add(EntityCreatedEvent.WithEntity(address));
        var _address = await _addressRepository.AddAsync(address, cancellationToken);
        var customer = new Customer(request.Name, request.Dob, request.Gender, request.Mail, request.PhoneNumber, _address.Id, request.UserId);
        // Add Domain Events to be raised after the commit
        customer.DomainEvents.Add(EntityCreatedEvent.WithEntity(customer));

        await _repository.AddAsync(customer, cancellationToken);

        return customer.Id;
    }
}