namespace Totostore.Backend.Application.Catalog.ProductPrices;

public class SearchProductPricesRequest: PaginationFilter, IRequest<PaginationResponse<ProductPriceDto>>
{
}

public class ProductPricesBySearchRequestSpec : EntitiesByPaginationFilterSpec<ProductPrice, ProductPriceDto>
{
    public ProductPricesBySearchRequestSpec(SearchProductPricesRequest request)
        : base(request)
    {
    }
}

public class SearchOrdersRequestHandler : IRequestHandler<SearchProductPricesRequest, PaginationResponse<ProductPriceDto>>
{
    private readonly IReadRepository<ProductPrice> _repository;

    public SearchOrdersRequestHandler(IReadRepository<ProductPrice> repository) => _repository = repository;

    public async Task<PaginationResponse<ProductPriceDto>> Handle(SearchProductPricesRequest request, CancellationToken cancellationToken)
    {
        var spec = new ProductPricesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}