namespace Totostore.Backend.Application.Catalog.Shippers;

public class SearchShippersRequest : PaginationFilter, IRequest<PaginationResponse<ShipperDto>>
{
}

public class ShippersBySearchRequestSpec : EntitiesByPaginationFilterSpec<Shipper, ShipperDto>
{
    public ShippersBySearchRequestSpec(SearchShippersRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchShippersRequestHandler : IRequestHandler<SearchShippersRequest, PaginationResponse<ShipperDto>>
{
    private readonly IReadRepository<Shipper> _repository;

    public SearchShippersRequestHandler(IReadRepository<Shipper> repository) => _repository = repository;

    public async Task<PaginationResponse<ShipperDto>> Handle(SearchShippersRequest request,
        CancellationToken cancellationToken)
    {
        var spec = new ShippersBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}