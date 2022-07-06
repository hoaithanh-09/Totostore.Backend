namespace Totostore.Backend.Application.Catalog.Categories;

public class SearchCategoriesRequest : PaginationFilter, IRequest<PaginationResponse<CategoryDto>>
{
}

public class CategoriesBySearchRequestSpec : EntitiesByPaginationFilterSpec<CategoryDetailDto, CategoryDto>
{
    public CategoriesBySearchRequestSpec(SearchCategoriesRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchCategoriesRequestHandler : IRequestHandler<SearchCategoriesRequest, PaginationResponse<CategoryDto>>
{
    private readonly IReadRepository<CategoryDetailDto> _repository;

    public SearchCategoriesRequestHandler(IReadRepository<CategoryDetailDto> repository) => _repository = repository;

    public async Task<PaginationResponse<CategoryDto>> Handle(SearchCategoriesRequest request,
        CancellationToken cancellationToken)
    {
        var spec = new CategoriesBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}