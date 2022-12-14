using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Application.Catalog.OrderStatus;

public class SearchOrderStatusRequest : PaginationFilter, IRequest<PaginationResponse<OrderStatusDto>>
{
}

public class OrderStatusBySearchRequestSpec : EntitiesByPaginationFilterSpec<Domain.Catalog.OrderStatus, OrderStatusDto>
{
    public OrderStatusBySearchRequestSpec(SearchOrderStatusRequest request)
        : base(request) =>
        Query
            .Include((x => x.Order));

}

public class SearchOrdersRequestHandler : IRequestHandler<SearchOrderStatusRequest, PaginationResponse<OrderStatusDto>>
{
    private readonly IReadRepository<Domain.Catalog.OrderStatus> _repository;

    public SearchOrdersRequestHandler(IReadRepository<Domain.Catalog.OrderStatus> repository) => _repository = repository;

    public async Task<PaginationResponse<OrderStatusDto>> Handle(SearchOrderStatusRequest request,
        CancellationToken cancellationToken)
    {
        var spec = new OrderStatusBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}