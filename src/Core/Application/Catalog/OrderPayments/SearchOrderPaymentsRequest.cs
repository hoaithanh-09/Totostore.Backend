namespace Totostore.Backend.Application.Catalog.OrderPayments;

public class SearchOrderPaymentsRequest : PaginationFilter, IRequest<PaginationResponse<OrderPaymentDto>>
{
}

public class OrderPaymentsBySearchRequestSpec : EntitiesByPaginationFilterSpec<OrderPayment, OrderPaymentDto>
{
    public OrderPaymentsBySearchRequestSpec(SearchOrderPaymentsRequest request)
        : base(request) => Query.OrderBy(x => x.CreatedOn);
}

public class SearchOrderPaymentsRequestHandler : IRequestHandler<SearchOrderPaymentsRequest,
    PaginationResponse<OrderPaymentDto>>
{
    private readonly IReadRepository<OrderPayment> _repository;

    public SearchOrderPaymentsRequestHandler(IReadRepository<OrderPayment> repository) =>
        _repository = repository;

    public async Task<PaginationResponse<OrderPaymentDto>> Handle(SearchOrderPaymentsRequest request,
        CancellationToken cancellationToken)
    {
        var spec = new OrderPaymentsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}