namespace Totostore.Backend.Application.Catalog.ProductImages;

public class SearchProductImagesRequest : PaginationFilter, IRequest<PaginationResponse<ProductImageDto>>
{
}

public class PtoductImagesBySearchRequestSpec : EntitiesByPaginationFilterSpec<ProductImage, ProductImageDto>
{
    public PtoductImagesBySearchRequestSpec(SearchProductImagesRequest request)
        : base(request) =>
        Query
            .Include((x => x.Product));

}

public class SearchOrdersRequestHandler : IRequestHandler<SearchProductImagesRequest, PaginationResponse<ProductImageDto>>
{
    private readonly IReadRepository<ProductImage> _repository;

    public SearchOrdersRequestHandler(IReadRepository<ProductImage> repository) => _repository = repository;

    public async Task<PaginationResponse<ProductImageDto>> Handle(SearchProductImagesRequest request,
        CancellationToken cancellationToken)
    {
        var spec = new PtoductImagesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}