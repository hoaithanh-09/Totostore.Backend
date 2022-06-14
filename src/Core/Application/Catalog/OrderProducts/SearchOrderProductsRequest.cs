using Totostore.Backend.Application.Catalog.Orders;

namespace Totostore.Backend.Application.Catalog.OrderProducts;

public class SearchOrderProductsRequest : PaginationFilter, IRequest<PaginationResponse<OrderProductDto>>
{
    public Guid? OrderId { get; set; }
}
public class OrderProductsBySearchRequestSpec : EntitiesByPaginationFilterSpec<OrderProduct, OrderProductDto>
{
    public OrderProductsBySearchRequestSpec(SearchOrderProductsRequest request)
        : base(request) => Query.Where((x => x.OrderId == request.OrderId.Value));
}

public class SearchOrderProductsRequestHandler : IRequestHandler<SearchOrderProductsRequest,
    PaginationResponse<OrderProductDto>>
{
    private readonly IReadRepository<OrderProduct> _repository;

    public SearchOrderProductsRequestHandler(IReadRepository<OrderProduct> repository) =>
        _repository = repository;

    public async Task<PaginationResponse<OrderProductDto>> Handle(SearchOrderProductsRequest request,
        CancellationToken cancellationToken)
    {
        var spec = new OrderProductsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}