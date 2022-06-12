using Totostore.Backend.Domain.Common.Events;

namespace Totostore.Backend.Application.Catalog.Payments;

public class UpdatePaymentRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}

public class UpdatePaymentRequestValidator : CustomValidator<UpdatePaymentRequest>
{
    public UpdatePaymentRequestValidator(IReadRepository<Payment> repository,
        IStringLocalizer<UpdatePaymentRequestValidator> localizer) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new PaymentByNameSpec(name), ct) is null)
            .WithMessage((_, name) => string.Format(localizer["payment.alreadyexists"], name));
}

public class UpdatePaymentRequestHandler : IRequestHandler<UpdatePaymentRequest, Guid>
{
    private readonly IStringLocalizer<UpdatePaymentRequestHandler> _localizer;

    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Payment> _repository;

    public UpdatePaymentRequestHandler(IRepositoryWithEvents<Payment> repository,
        IStringLocalizer<UpdatePaymentRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<Guid> Handle(UpdatePaymentRequest request, CancellationToken cancellationToken)
    {
        var payment = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = payment ?? throw new NotFoundException(string.Format(_localizer["payment.notfound"], request.Id));

        payment.Update(request.Name);
        // Add Domain Events to be raised after the commit
        payment.DomainEvents.Add(EntityUpdatedEvent.WithEntity(payment));

        await _repository.UpdateAsync(payment, cancellationToken);

        return request.Id;
    }
}