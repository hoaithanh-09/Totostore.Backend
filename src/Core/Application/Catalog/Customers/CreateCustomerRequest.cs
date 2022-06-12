using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Customers;

public class CreateCustomerRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public DateTime Dob { get; set; } = default!;
    public bool Gender { get; set; }
    public string Mail { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public Guid AddressId { get; set; }
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

    public CreateCustomerRequestHandler(IRepository<Customer> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = new Customer(request.Name, request.Dob, request.Gender, request.Mail, request.PhoneNumber,
            request.AddressId);

        // Add Domain Events to be raised after the commit
        customer.DomainEvents.Add(EntityCreatedEvent.WithEntity(customer));

        await _repository.AddAsync(customer, cancellationToken);

        return customer.Id;
    }
}