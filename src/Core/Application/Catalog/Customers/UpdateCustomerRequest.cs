using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Customers;

public class UpdateCustomerRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
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

public class UpdateCustomerRequestValidator : CustomValidator<UpdateCustomerRequest>
{
    public UpdateCustomerRequestValidator(IReadRepository<Customer> repository,
        IStringLocalizer<UpdateCustomerRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new CustomerByNameSpec(name), ct) is null)
            .WithMessage((_, name) => string.Format(localizer["customer.alreadyexists"], name));
}

public class UpdateCustomerRequestHandler : IRequestHandler<UpdateCustomerRequest, Guid>
{
    private readonly IStringLocalizer<UpdateCustomerRequestHandler> _localizer;

    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Customer> _repository;
    private readonly IRepositoryWithEvents<Address> _addressRepository;
    public UpdateCustomerRequestHandler(IRepositoryWithEvents<Customer> repository,
        IStringLocalizer<UpdateCustomerRequestHandler> localizer, IRepositoryWithEvents<Address> addressRepository) =>
        (_repository, _localizer, _addressRepository) = (repository, localizer, addressRepository);

    public async Task<Guid> Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = customer ?? throw new NotFoundException(string.Format(_localizer["customer.notfound"], request.Id));
        var address= await _addressRepository.GetByIdAsync(customer.AddressId, cancellationToken);
        address.Update(request.City, request.District, request.Ward, request.StayingAddress);
        customer.Update(request.Name, request.Dob, request.Gender, request.Mail, request.PhoneNumber,
           customer.AddressId, request.UserId);
        // Add Domain Events to be raised after the commit
        customer.DomainEvents.Add(EntityUpdatedEvent.WithEntity(customer));

        await _repository.UpdateAsync(customer, cancellationToken);
        address.DomainEvents.Add(EntityUpdatedEvent.WithEntity(address));

        await _addressRepository.UpdateAsync(address, cancellationToken);

        return request.Id;
    }
}