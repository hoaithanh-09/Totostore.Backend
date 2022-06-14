using Totostore.Backend.Shared.Enums;

namespace Totostore.Backend.Application.Catalog.OrderStatuses;

public class SearchOrderStatusRequest : PaginationFilter, IRequest<PaginationResponse<OrderStatusDto>>
{
    public OrderStatusEnums? Status { get; set; }
    public Guid? OrderId { get; set; }
}

public class OrderStatusBySearchRequestSpec : EntitiesByPaginationFilterSpec<OrderStatus, OrderStatusDto>
{
    public OrderStatusBySearchRequestSpec(SearchOrderStatusRequest request)
        : base(request) =>
        Query
            .Include((x => x.Order))
            .Where(x => x.Status == request.Status.Value)
            .Where(x => x.OrderId == request.OrderId.Value);
}

public class SearchOrdersRequestHandler : IRequestHandler<SearchOrderStatusRequest, PaginationResponse<OrderStatusDto>>
{
    private readonly IReadRepository<OrderStatus> _repository;

    public SearchOrdersRequestHandler(IReadRepository<OrderStatus> repository) => _repository = repository;

    public async Task<PaginationResponse<OrderStatusDto>> Handle(SearchOrderStatusRequest request,
        CancellationToken cancellationToken)
    {
        var spec = new OrderStatusBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}