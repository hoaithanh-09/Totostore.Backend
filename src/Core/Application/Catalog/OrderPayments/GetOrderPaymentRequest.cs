namespace Totostore.Backend.Application.Catalog.OrderPayments;

public class GetOrderPaymentRequest : IRequest<OrderPaymentDetailsDto>
{
    public GetOrderPaymentRequest(Guid id) => Id = id;
    public Guid Id { get; set; }
}

public class OrderPaymentByIdSpec : Specification<OrderPayment, OrderPaymentDto>, ISingleResultSpecification
{
    public OrderPaymentByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id)
            .Include(p => p.Order)
            .Include(p => p.Payment);
}

public class GetOrderPaymentRequestHandler : IRequestHandler<GetOrderPaymentRequest, OrderPaymentDetailsDto>
{
    private readonly IStringLocalizer<GetOrderPaymentRequestHandler> _localizer;
    private readonly IRepository<OrderPayment> _repository;

    public GetOrderPaymentRequestHandler(IRepository<OrderPayment> repository,
        IStringLocalizer<GetOrderPaymentRequestHandler> localizer) =>
        (_repository, _localizer) = (repository, localizer);

    public async Task<OrderPaymentDetailsDto> Handle(GetOrderPaymentRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<OrderPayment, OrderPaymentDetailsDto>)new OrderPaymentByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(string.Format(_localizer["orderPayment.notfound"], request.Id));
}