namespace Totostore.Backend.Application.Catalog.CategoryProducts;

public class SearchCategoryProductsRequest : PaginationFilter, IRequest<PaginationResponse<CategoryProductDto>>
{
}

public class CategoryProductsBySearchRequestSpec : EntitiesByPaginationFilterSpec<CategoryProduct, CategoryProductDto>
{
    public CategoryProductsBySearchRequestSpec(SearchCategoryProductsRequest request)
        : base(request) => Query.OrderBy(x=>x.CreatedOn);
}

public class SearchCategoryProductsRequestHandler : IRequestHandler<SearchCategoryProductsRequest,
    PaginationResponse<CategoryProductDto>>
{
    private readonly IReadRepository<CategoryProduct> _repository;

    public SearchCategoryProductsRequestHandler(IReadRepository<CategoryProduct> repository) =>
        _repository = repository;

    public async Task<PaginationResponse<CategoryProductDto>> Handle(SearchCategoryProductsRequest request,
        CancellationToken cancellationToken)
    {
        var spec = new CategoryProductsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}

