namespace Totostore.Backend.Application.Catalog.Payments;

public class SearchPaymentsRequest : PaginationFilter, IRequest<PaginationResponse<PaymentDto>>
{
}

public class PaymentsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Payment, PaymentDto>
{
    public PaymentsBySearchRequestSpec(SearchPaymentsRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchPaymentsRequestHandler : IRequestHandler<SearchPaymentsRequest, PaginationResponse<PaymentDto>>
{
    private readonly IReadRepository<Payment> _repository;

    public SearchPaymentsRequestHandler(IReadRepository<Payment> repository) => _repository = repository;

    public async Task<PaginationResponse<PaymentDto>> Handle(SearchPaymentsRequest request,
        CancellationToken cancellationToken)
    {
        var spec = new PaymentsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}