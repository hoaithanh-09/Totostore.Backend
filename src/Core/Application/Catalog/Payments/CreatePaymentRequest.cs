using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Payments;

public class CreatePaymentRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
}

public class CreatePaymentRequestValidator : CustomValidator<CreatePaymentRequest>
{
    public CreatePaymentRequestValidator(IReadRepository<Payment> repository,
        IStringLocalizer<CreatePaymentRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new PaymentByNameSpec(name), ct) is null)
            .WithMessage((_, name) => string.Format(localizer["payment.alreadyexists"], name));
}

public class CreatePaymentRequestHandler : IRequestHandler<CreatePaymentRequest, Guid>
{
    private readonly IFileStorageService _file;
    private readonly IRepository<Payment> _repository;

    public CreatePaymentRequestHandler(IRepository<Payment> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(CreatePaymentRequest request, CancellationToken cancellationToken)
    {
        var payment = new Payment(request.Name);

        // Add Domain Events to be raised after the commit
        payment.DomainEvents.Add(EntityCreatedEvent.WithEntity(payment));

        await _repository.AddAsync(payment, cancellationToken);

        return payment.Id;
    }
}