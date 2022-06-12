using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Suppliers;

public class CreateSupplierRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}

public class CreateSupplierRequestValidator : CustomValidator<CreateSupplierRequest>
{
    public CreateSupplierRequestValidator(IReadRepository<Supplier> repository,
        IStringLocalizer<CreateSupplierRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new SupplierByNameSpec(name), ct) is null)
            .WithMessage((_, name) => string.Format(localizer["supplier.alreadyexists"], name));
}

public class CreateSupplierRequestHandler : IRequestHandler<CreateSupplierRequest, Guid>
{
    private readonly IFileStorageService _file;
    private readonly IRepository<Supplier> _repository;

    public CreateSupplierRequestHandler(IRepository<Supplier> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(CreateSupplierRequest request, CancellationToken cancellationToken)
    {
        var supplier = new Supplier(request.Name, request.Description);

        // Add Domain Events to be raised after the commit
        supplier.DomainEvents.Add(EntityCreatedEvent.WithEntity(supplier));

        await _repository.AddAsync(supplier, cancellationToken);

        return supplier.Id;
    }
}