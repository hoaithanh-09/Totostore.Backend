using Totostore.Backend.Application.Catalog.Customers;

namespace Totostore.Backend.Application.Catalog.Payments;

public class GetPaymentRequest : IRequest<PaymentDto>
{
    public GetPaymentRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class PaymentByIdSpec : Specification<Payment, PaymentDto>, ISingleResultSpecification
{
    public PaymentByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetPaymentRequestHandler : IRequestHandler<GetPaymentRequest, PaymentDto>
{
    private readonly IStringLocalizer<GetPaymentRequestHandler> _localizer;
    private readonly IRepository<Payment> _repository;

    public GetPaymentRequestHandler(IRepository<Payment> repository,
        IStringLocalizer<GetPaymentRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<PaymentDto> Handle(GetPaymentRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Payment, PaymentDto>)new PaymentByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["payment.notfound"], request.Id));
}