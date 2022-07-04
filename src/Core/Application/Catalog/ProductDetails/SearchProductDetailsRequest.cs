namespace Totostore.Backend.Application.Catalog.ProductDetails;

public class SearchProductDetailsRequest : PaginationFilter, IRequest<PaginationResponse<ProductDetailDto>>
{
}

public class ProductDetailsBySearchRequestSpec : EntitiesByPaginationFilterSpec<ProductDetail, ProductDetailDto>
{
    public ProductDetailsBySearchRequestSpec(SearchProductDetailsRequest request)
        : base(request) => Query.OrderBy(x => x.CreatedOn);
}

public class SearchProductDetailsRequestHandler : IRequestHandler<SearchProductDetailsRequest,
    PaginationResponse<ProductDetailDto>>
{
    private readonly IReadRepository<ProductDetail> _repository;

    public SearchProductDetailsRequestHandler(IReadRepository<ProductDetail> repository) =>
        _repository = repository;

    public async Task<PaginationResponse<ProductDetailDto>> Handle(SearchProductDetailsRequest request,
        CancellationToken cancellationToken)
    {
        var spec = new ProductDetailsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}