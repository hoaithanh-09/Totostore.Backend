using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Suppliers;

public class UpdateSupplierRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}

public class UpdateSupplierRequestValidator : CustomValidator<UpdateSupplierRequest>
{
    public UpdateSupplierRequestValidator(IReadRepository<Supplier> repository,
        IStringLocalizer<UpdateSupplierRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new SupplierByNameSpec(name), ct) is null)
            .WithMessage((_, name) => string.Format(localizer["supplier.alreadyexists"], name));
}

public class UpdateSupplierRequestHandler : IRequestHandler<UpdateSupplierRequest, Guid>
{
    private readonly IStringLocalizer<UpdateSupplierRequestHandler> _localizer;

    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Supplier> _repository;

    public UpdateSupplierRequestHandler(IRepositoryWithEvents<Supplier> repository,
        IStringLocalizer<UpdateSupplierRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(UpdateSupplierRequest request, CancellationToken cancellationToken)
    {
        var supplier = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = supplier ?? throw new NotFoundException(string.Format(_localizer["supplier.notfound"], request.Id));

        supplier.Update(request.Name, request.Description);
        // Add Domain Events to be raised after the commit
        supplier.DomainEvents.Add(EntityUpdatedEvent.WithEntity(supplier));

        await _repository.UpdateAsync(supplier, cancellationToken);

        return request.Id;
    }
}